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
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
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

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Color parentColor = this.Parent != null ? this.Parent.BackColor : Color.FromArgb(15, 23, 42);
            g.Clear(parentColor);

            Rectangle rectSurface = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            Rectangle rectBorder = new Rectangle(1, 1, this.Width - 3, this.Height - 3);

            using (GraphicsPath pathSurface = GetRoundedPath(rectSurface, borderRadius))
            using (GraphicsPath pathBorder = GetRoundedPath(rectBorder, borderRadius - 1))
            {
                Color currentBg = (this.Image == null && isHovered) ? hoverBackColor : backColor;
                using (SolidBrush bgBrush = new SolidBrush(currentBg))
                {
                    g.FillPath(bgBrush, pathSurface);
                }

                if (this.Image != null)
                {
                    g.SetClip(pathBorder);
                    DrawImageWithSizeMode(g, this.Image, rectBorder);
                    g.ResetClip();

                    Color activeBorder = isHovered ? accentColor : normalBorderColor;
                    using (Pen borderPen = new Pen(activeBorder, 2f))
                    {
                        g.DrawPath(borderPen, pathBorder);
                    }
                }
                else
                {
                    Color dashColor = isHovered ? accentColor : normalBorderColor;
                    using (Pen dashedPen = new Pen(dashColor, 2f))
                    {
                        dashedPen.DashStyle = DashStyle.Dash;
                        dashedPen.DashPattern = new float[] { 4f, 3f };
                        g.DrawPath(dashedPen, pathBorder);
                    }

                    DrawPlaceholderContent(g);
                }
            }
        }

        private void DrawPlaceholderContent(Graphics g)
        {
            Color iconCol = isHovered ? accentColor : placeholderColor;
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
                // رسم الصورة بحجمها الحقيقي (مثلاً 100×100) في المنتصف تماماً دون تكبير
                int posX = bounds.X + (bounds.Width - img.Width) / 2;
                int posY = bounds.Y + (bounds.Height - img.Height) / 2;
                g.DrawImage(img, new Rectangle(posX, posY, img.Width, img.Height));
            }
            else // Zoom أو Normal
            {
                // إذا كانت الصورة أصغر من الصندوق (مثل الأيقونة 100×100)، نضعها في المنتصف ولا نكبرها!
                if (img.Width <= bounds.Width && img.Height <= bounds.Height)
                {
                    int posX = bounds.X + (bounds.Width - img.Width) / 2;
                    int posY = bounds.Y + (bounds.Height - img.Height) / 2;
                    g.DrawImage(img, new Rectangle(posX, posY, img.Width, img.Height));
                }
                else
                {
                    // أما إذا كانت صورة حقيقية كبيرة، نصغرها لتناسب الصندوق بأناقة
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