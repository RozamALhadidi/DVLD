using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DVLD
{
    public class SlateDarkButton : Button
    {
        private readonly int _BorderRadius = 18;
        private readonly int _BorderSize = 2;

        private bool isHovered = false;
        private bool isPressed = false;

        private readonly Color normalBack = Color.FromArgb(30, 41, 59);
        private readonly Color hoverBack = Color.FromArgb(40, 54, 77);
        private readonly Color clickBack = Color.FromArgb(15, 23, 42);

        private readonly Color normalBorder = Color.FromArgb(51, 65, 85);
        private readonly Color accentSkyBlue = Color.FromArgb(56, 189, 248);
        private readonly Color clickBorder = Color.FromArgb(2, 132, 199);

        private readonly Color textLight = Color.FromArgb(248, 250, 252);

        // ألوان حالة التعطيل (Enabled = false)
        private readonly Color disabledBack = Color.FromArgb(20, 28, 43);
        private readonly Color disabledBorder = Color.FromArgb(38, 49, 66);
        private readonly Color disabledText = Color.FromArgb(90, 105, 125);

        public SlateDarkButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = normalBack;
            this.ForeColor = textLight;
            this.Font = new Font("Alexandria", 11F, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
            this.Size = new Size(150, 42);

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!this.Enabled)
            {
                isHovered = false;
                isPressed = false;
            }
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!this.Enabled) return;
            isHovered = true;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            isPressed = false;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (!this.Enabled) return;
            isPressed = true;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            isPressed = false;
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

        protected override bool ShowFocusCues => false;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color cornerColor = (this.Parent != null && this.Parent.BackColor != Color.Transparent)
                                ? this.Parent.BackColor
                                : clickBack;
            g.Clear(cornerColor);

            Color fillColor;
            Color borderColor;
            Color textColor;

            if (!this.Enabled)
            {
                fillColor = disabledBack;
                borderColor = disabledBorder;
                textColor = disabledText;
            }
            else
            {
                bool isActive = isHovered || this.Focused;
                fillColor = isPressed ? clickBack : (isActive ? hoverBack : normalBack);
                borderColor = isPressed ? clickBorder : (isActive ? accentSkyBlue : normalBorder);
                textColor = (isActive || isPressed) ? accentSkyBlue : textLight;
            }

            Rectangle rect = new Rectangle(2, 2, this.Width - 5, this.Height - 5);

            // رسم هالة التوهج فقط عندما يكون الزر مفعّلاً
            if (this.Enabled && this.Focused && !isPressed)
            {
                Rectangle glowRect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
                using (GraphicsPath glowPath = GetRoundedPath(glowRect, _BorderRadius + 1))
                using (Pen glowPen = new Pen(Color.FromArgb(90, accentSkyBlue), 4))
                {
                    g.DrawPath(glowPen, glowPath);
                }
            }

            using (GraphicsPath path = GetRoundedPath(rect, _BorderRadius))
            using (SolidBrush brush = new SolidBrush(fillColor))
            using (Pen pen = new Pen(borderColor, _BorderSize))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }

            Size textSize = TextRenderer.MeasureText(this.Text, this.Font);

            if (this.Image != null)
            {
                int imageWidth = this.Image.Width;
                int spacing = 8;
                int totalWidth = imageWidth + spacing + textSize.Width;

                int startX = (this.Width - totalWidth) / 2;
                int imageY = (this.Height - this.Image.Height) / 2;

                // تقليل شفافية الأيقونة أيضاً عند التعطيل لتبدو باهتة بوضوح
                Color iconTint = this.Enabled ? textColor : Color.FromArgb(120, disabledText);

                using (ImageAttributes imageAttr = GetTintedImageAttributes(iconTint))
                {
                    Rectangle destRect = new Rectangle(startX, imageY, imageWidth, this.Image.Height);
                    g.DrawImage(this.Image, destRect, 0, 0, this.Image.Width, this.Image.Height, GraphicsUnit.Pixel, imageAttr);
                }

                int textX = startX + imageWidth + spacing;
                int textY = (this.Height - textSize.Height) / 2;
                TextRenderer.DrawText(g, this.Text, this.Font, new Point(textX, textY), textColor);
            }
            else
            {
                TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, textColor, flags);
            }
        }

        private ImageAttributes GetTintedImageAttributes(Color tintColor)
        {
            float r = tintColor.R / 255f;
            float g = tintColor.G / 255f;
            float b = tintColor.B / 255f;
            float a = tintColor.A / 255f;

            float[][] colorMatrixElements = {
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, a, 0},
                new float[] {r, g, b, 0, 1}
            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            return attributes;
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float r = radius * 2f;
            if (r > rect.Width) r = rect.Width;
            if (r > rect.Height) r = rect.Height;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}