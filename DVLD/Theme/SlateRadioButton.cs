using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DVLD
{
    public class SlateRadioButton : RadioButton
    {
        private bool isHovered = false;

        private readonly Color circleBorderColor = Color.FromArgb(51, 65, 85);
        private readonly Color accentSkyBlue = Color.FromArgb(56, 189, 248);
        private readonly Color textColor = Color.FromArgb(241, 245, 249);

        public SlateRadioButton()
        {
            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);

            this.Cursor = Cursors.Hand;
            this.BackColor = Color.FromArgb(15, 23, 42);
        }

        protected override bool ShowFocusCues => false;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color bgColor = (this.BackColor != Color.Transparent)
                            ? this.BackColor
                            : (this.Parent != null ? this.Parent.BackColor : Color.FromArgb(15, 23, 42));
            g.Clear(bgColor);

            bool isActive = isHovered || this.Focused;

            int circleSize = 14;
            Rectangle rect = new Rectangle(4, (this.Height - circleSize) / 2, circleSize, circleSize);

            if (this.Focused)
            {
                Rectangle glowRect = new Rectangle(rect.X - 2, rect.Y - 2, rect.Width + 4, rect.Height + 4);
                using (Pen glowPen = new Pen(Color.FromArgb(95, accentSkyBlue), 3f))
                {
                    g.DrawEllipse(glowPen, glowRect);
                }
            }

            Color currentBorder = (this.Checked || isActive) ? accentSkyBlue : circleBorderColor;
            using (Pen pen = new Pen(currentBorder, 2f))
            {
                g.DrawEllipse(pen, rect);
            }

            if (this.Checked)
            {
                Rectangle innerRect = new Rectangle(rect.X + 3, rect.Y + 3, rect.Width - 6, rect.Height - 6);
                using (SolidBrush brush = new SolidBrush(accentSkyBlue))
                {
                    g.FillEllipse(brush, innerRect);
                }
            }

            Color currentTextColor = isActive ? accentSkyBlue : textColor;
            using (SolidBrush textBrush = new SolidBrush(currentTextColor))
            {
                PointF textLocation = new PointF(24, (this.Height - this.Font.Height) / 2f);
                g.DrawString(this.Text, this.Font, textBrush, textLocation);
            }
        }
    }
}