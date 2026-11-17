using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            isPressed = true;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            isPressed = false;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color cornerColor = (this.Parent != null && this.Parent.BackColor != Color.Transparent)
                                ? this.Parent.BackColor
                                : clickBack;
            g.Clear(cornerColor);

            Color fillColor = isPressed ? clickBack : (isHovered ? hoverBack : normalBack);
            Color borderColor = isPressed ? clickBorder : (isHovered ? accentSkyBlue : normalBorder);
            Color textColor = (isHovered || isPressed) ? accentSkyBlue : textLight;

            Rectangle rect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);

            using (GraphicsPath path = GetRoundedPath(rect, _BorderRadius))
            using (SolidBrush brush = new SolidBrush(fillColor))
            using (Pen pen = new Pen(borderColor, _BorderSize))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }

            // --- التعديل هنا لرسم الصورة والنص معاً ---

            // 1. حساب مساحة النص
            Size textSize = TextRenderer.MeasureText(this.Text, this.Font);

            // 2. إذا كان هناك صورة
            if (this.Image != null)
            {
                // حساب العرض الإجمالي (الصورة + المسافة + النص)
                int imageWidth = this.Image.Width;
                int spacing = 8; // المسافة بين الصورة والنص
                int totalWidth = imageWidth + spacing + textSize.Width;

                // حساب نقطة البداية ليكون المحتوى بالمنتصف
                int startX = (this.Width - totalWidth) / 2;

                // رسم الصورة في المنتصف عمودياً
                int imageY = (this.Height - this.Image.Height) / 2;
                g.DrawImage(this.Image, startX, imageY, imageWidth, this.Image.Height);

                // رسم النص بجانب الصورة
                int textX = startX + imageWidth + spacing;
                int textY = (this.Height - textSize.Height) / 2;
                TextRenderer.DrawText(g, this.Text, this.Font, new Point(textX, textY), textColor);
            }
            else
            {
                // إذا لم يكن هناك صورة، ارسم النص في المنتصف كالعادة
                TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, textColor, flags);
            }
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