using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace DVLD
{
    public class SlatePictureBox : PictureBox
    {
        private Color backColor = Color.FromArgb(30, 41, 59);
        private Color hoverBackColor = Color.FromArgb(36, 49, 70);
        private Color normalBorderColor = Color.FromArgb(51, 65, 85);
        private Color accentColor = Color.FromArgb(56, 189, 248);
        private Color placeholderColor = Color.FromArgb(148, 163, 184);

        private bool isHovered = false;
        private int borderRadius = 12;
        private string placeholderText = "No Image";

        [Category("Slate Theme")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; this.Invalidate(); }
        }

        [Category("Slate Theme")]
        public string PlaceholderText
        {
            get { return placeholderText; }
            set { placeholderText = value; this.Invalidate(); }
        }

        public SlatePictureBox()
        {
            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable, true); // تفعيل استقبال التركيز

            this.TabStop = true; // السماح بالوصول إليه عبر زر Tab
            this.Cursor = Cursors.Hand;
            this.BackColor = backColor;
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.BorderStyle = BorderStyle.None;
            this.Size = new Size(150, 150);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            this.Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }

        // السماح بتشغيل حدث Click عند الضغط على Enter أو Space أثناء التركيز
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                this.OnClick(EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Color parentColor = this.Parent != null ? this.Parent.BackColor : Color.FromArgb(15, 23, 42);
            g.Clear(parentColor);

            bool isActive = isHovered || this.Focused;

            Rectangle rectSurface = new Rectangle(2, 2, this.Width - 5, this.Height - 5);
            Rectangle rectBorder = new Rectangle(2, 2, this.Width - 5, this.Height - 5);

            // رسم هالة توهج خارجية باللون الأزرق الفاتح عند التركيز بالـ Tab
            if (this.Focused)
            {
                Rectangle glowRect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
                using (GraphicsPath glowPath = GetRoundedPath(glowRect, borderRadius + 1))
                using (Pen glowPen = new Pen(Color.FromArgb(90, accentColor), 4))
                {
                    g.DrawPath(glowPen, glowPath);
                }
            }

            using (GraphicsPath pathSurface = GetRoundedPath(rectSurface, borderRadius))
            using (GraphicsPath pathBorder = GetRoundedPath(rectBorder, borderRadius - 1))
            {
                Color currentBg = (this.Image == null && isActive) ? hoverBackColor : backColor;
                using (SolidBrush bgBrush = new SolidBrush(currentBg))
                {
                    g.FillPath(bgBrush, pathSurface);
                }

                if (this.Image != null)
                {
                    g.SetClip(pathBorder);
                    DrawImageWithSizeMode(g, this.Image, rectBorder);
                    g.ResetClip();

                    Color activeBorder = isActive ? accentColor : normalBorderColor;
                    using (Pen borderPen = new Pen(activeBorder, 2f))
                    {
                        g.DrawPath(borderPen, pathBorder);
                    }
                }
                else
                {
                    Color dashColor = isActive ? accentColor : normalBorderColor;
                    using (Pen dashedPen = new Pen(dashColor, 2f))
                    {
                        dashedPen.DashStyle = DashStyle.Dash;
                        dashedPen.DashPattern = new float[] { 4f, 3f };
                        g.DrawPath(dashedPen, pathBorder);
                    }

                    DrawPlaceholderContent(g, isActive);
                }
            }
        }

        private void DrawPlaceholderContent(Graphics g, bool isActive)
        {
            Color iconCol = isActive ? accentColor : placeholderColor;
            int centerX = this.Width / 2;
            int centerY = (this.Height / 2) - 10;

            using (Pen iconPen = new Pen(iconCol, 2.2f))
            {
                iconPen.StartCap = LineCap.Round;
                iconPen.EndCap = LineCap.Round;

                int headSize = 22;
                g.DrawEllipse(iconPen, centerX - (headSize / 2), centerY - 22, headSize, headSize);

                int bodyWidth = 42;
                int bodyHeight = 26;
                g.DrawArc(iconPen, centerX - (bodyWidth / 2), centerY + 4, bodyWidth, bodyHeight, 180, 180);

                using (Pen plusPen = new Pen(accentColor, 2f))
                {
                    int plusX = centerX + 18;
                    int plusY = centerY - 18;
                    g.DrawLine(plusPen, plusX - 4, plusY, plusX + 4, plusY);
                    g.DrawLine(plusPen, plusX, plusY - 4, plusX, plusY + 4);
                }
            }

            using (Font textFont = new Font("Segoe UI", 9.5f, FontStyle.Regular))
            using (SolidBrush textBrush = new SolidBrush(iconCol))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(placeholderText, textFont, textBrush, new PointF(centerX, centerY + 32), sf);
            }
        }

        private void DrawImageWithSizeMode(Graphics g, Image img, Rectangle bounds)
        {
            if (this.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                g.DrawImage(img, bounds);
            }
            else if (this.SizeMode == PictureBoxSizeMode.CenterImage)
            {
                int posX = bounds.X + (bounds.Width - img.Width) / 2;
                int posY = bounds.Y + (bounds.Height - img.Height) / 2;
                g.DrawImage(img, new Rectangle(posX, posY, img.Width, img.Height));
            }
            else
            {
                if (img.Width <= bounds.Width && img.Height <= bounds.Height)
                {
                    int posX = bounds.X + (bounds.Width - img.Width) / 2;
                    int posY = bounds.Y + (bounds.Height - img.Height) / 2;
                    g.DrawImage(img, new Rectangle(posX, posY, img.Width, img.Height));
                }
                else
                {
                    float ratioX = (float)bounds.Width / img.Width;
                    float ratioY = (float)bounds.Height / img.Height;
                    float ratio = Math.Max(ratioX, ratioY);

                    int newWidth = (int)(img.Width * ratio);
                    int newHeight = (int)(img.Height * ratio);
                    int posX = bounds.X + (bounds.Width - newWidth) / 2;
                    int posY = bounds.Y + (bounds.Height - newHeight) / 2;

                    g.DrawImage(img, new Rectangle(posX, posY, newWidth, newHeight));
                }
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            if (diameter <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            Rectangle arc = new Rectangle(rect.Location, new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        public void LoadImageSafe(string imagePath, Image defaultImg = null)
        {
            if (this.Image != null)
            {
                this.Image.Dispose();
                this.Image = null;
            }

            if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
            {
                try
                {
                    byte[] bytes = File.ReadAllBytes(imagePath);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    using (Image temp = Image.FromStream(ms))
                    {
                        this.Image = new Bitmap(temp);
                    }
                }
                catch
                {
                    this.Image = defaultImg != null ? new Bitmap(defaultImg) : null;
                }
            }
            else
            {
                this.Image = defaultImg != null ? new Bitmap(defaultImg) : null;
            }

            this.Invalidate();
        }

        public static Image ResizeImage(Image originalImage, int width, int height)
        {
            if (originalImage == null) return null;

            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                g.DrawImage(originalImage, 0, 0, width, height);
            }
            return resizedImage;
        }
    }
}