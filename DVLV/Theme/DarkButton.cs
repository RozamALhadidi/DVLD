using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DVLV // تأكد من تطابق اسم المشروع
{
    public class DarkButton : Button
    {
        private int _BorderRadius = 18; // دوران الحواف
        private int _BorderSize = 2;    // سماكة الحواف

        private bool isHovered = false;
        private bool isPressed = false;

        public DarkButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = AppTheme.InnerGroupBoxColor;
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
            this.Size = new Size(150, 40);

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

            // 1. مسح الزوايا الخلفية بنفس لون الجروب بوكس الداخلي تماماً لتختفي الزوايا
            Color cornerColor = (this.Parent != null && this.Parent.BackColor != Color.Transparent)
                                ? this.Parent.BackColor
                                : AppTheme.InnerGroupBoxColor;
            g.Clear(cornerColor);

            // 2. تحديد لون الزر ولون الحواف حسب حالة الماوس
            Color fillColor;
            Color borderColor;

            if (isPressed)
            {
                // لما تكبس عليه: يصير الزر نفسه أسود
                fillColor = AppTheme.ButtonClickColor;
                borderColor = AppTheme.BorderClickColor;
            }
            else if (isHovered)
            {
                // لما تحط المؤشر: لونه أفتح بشوي والحواف أبيض
                fillColor = AppTheme.ButtonHoverColor;
                borderColor = AppTheme.BorderHoverColor;
            }
            else
            {
                // الوضع العادي: لونه رمادي وحوافه سوداء
                fillColor = AppTheme.ButtonNormalColor;
                borderColor = AppTheme.BorderNormalColor;
            }

            // 3. ضبط أبعاد الرسم داخل حدود الزر
            Rectangle rect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);

            using (GraphicsPath path = GetRoundedPath(rect, _BorderRadius))
            {
                // تلوين داخل الزر
                using (SolidBrush brush = new SolidBrush(fillColor))
                {
                    g.FillPath(brush, path);
                }

                // رسم الحواف
                using (Pen pen = new Pen(borderColor, _BorderSize))
                {
                    g.DrawPath(pen, path);
                }
            }

            // 4. رسم النص في المنتصف
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, flags);
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