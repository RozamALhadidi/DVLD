using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DVLD
{
    public class SlateComboBox : ComboBox
    {
        private const int WM_PAINT = 0xF;
        private int buttonWidth = SystemInformation.VerticalScrollBarWidth;
        private bool isHovered = false;

        private readonly Color normalBorder = Color.FromArgb(51, 65, 85);
        private readonly Color accentSkyBlue = Color.FromArgb(56, 189, 248);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        public SlateComboBox()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.FromArgb(30, 41, 59);
            this.ForeColor = Color.FromArgb(248, 250, 252);
        }
        private DateTime _lastDropDownClosed = DateTime.MinValue;

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            _lastDropDownClosed = DateTime.Now;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left && !this.DroppedDown &&
                (DateTime.Now - _lastDropDownClosed).TotalMilliseconds > 150)
            {
                this.DroppedDown = true;
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetWindowTheme(this.Handle, "", "");
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

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    bool isActive = isHovered || this.Focused || this.DroppedDown;

                    Rectangle buttonRect = new Rectangle(this.Width - buttonWidth - 2, 0, buttonWidth + 2, this.Height);
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                    {
                        g.FillRectangle(brush, buttonRect);
                    }

                    using (SolidBrush arrowBrush = new SolidBrush(accentSkyBlue))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        int arrowWidth = 10;
                        int arrowHeight = 5;
                        int x = buttonRect.X + (buttonRect.Width - arrowWidth) / 2;
                        int y = buttonRect.Y + (buttonRect.Height - arrowHeight) / 2 + 1;

                        Point[] arrowPoints = new Point[]
                        {
                    new Point(x, y),
                    new Point(x + arrowWidth, y),
                    new Point(x + (arrowWidth / 2), y + arrowHeight)
                        };

                        g.FillPolygon(arrowBrush, arrowPoints);
                    }

                    if (this.Focused || this.DroppedDown)
                    {
                        using (Pen glowPen = new Pen(Color.FromArgb(90, accentSkyBlue), 4))
                        {
                            g.DrawRectangle(glowPen, 1, 1, this.Width - 3, this.Height - 3);
                        }
                    }

                    Color borderColor = isActive ? accentSkyBlue : normalBorder;
                    using (Pen pen = new Pen(borderColor, 2))
                    {
                        g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }
    }
}