using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DVLD
{
    public class SlateTextBox : TextBox
    {
        private const int WM_PAINT = 0x000F;
        private const int WM_NCPAINT = 0x0085;
        private const uint RDW_INVALIDATE = 0x0001;
        private const uint RDW_FRAME = 0x0400;
        private const uint RDW_UPDATENOW = 0x0100;

        private Color normalBackColor = Color.FromArgb(30, 41, 59);       // الخلفية العادية (#1E293B)
        private Color focusBackColor = Color.FromArgb(40, 54, 77);        // خلفية أفتح عند الكتابة
        private Color normalBorderColor = Color.FromArgb(51, 65, 85);     // حواف عادية (#334155)
        private Color focusBorderColor = Color.FromArgb(56, 189, 248);    // حواف زرقاء سماوية (#38BDF8)

        private bool isHovered = false;

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        public SlateTextBox()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = normalBackColor;
            this.ForeColor = Color.FromArgb(248, 250, 252);
            this.Font = new Font("Alexandria", 10f, FontStyle.Regular);
        }

        private void RedrawBorder()
        {
            if (this.IsHandleCreated)
            {
                // إجبار ويندوز على إعادة رسم الإطار الخارجي (Non-Client Area) فوراً
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_INVALIDATE | RDW_UPDATENOW);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            RedrawBorder();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            RedrawBorder();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.BackColor = focusBackColor;
            RedrawBorder();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.BackColor = normalBackColor;
            RedrawBorder();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCPAINT || m.Msg == WM_PAINT)
            {
                IntPtr hdc = GetWindowDC(this.Handle);
                if (hdc != IntPtr.Zero)
                {
                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        bool isActive = this.Focused || isHovered;
                        Color currentBorder = isActive ? focusBorderColor : normalBorderColor;

                        // رسم هالة توهج ناعمة عند التركيز
                        if (this.Focused)
                        {
                            using (Pen glowPen = new Pen(Color.FromArgb(90, focusBorderColor), 3))
                            {
                                glowPen.Alignment = PenAlignment.Inset;
                                g.DrawRectangle(glowPen, 0, 0, this.Width - 1, this.Height - 1);
                            }
                        }

                        // رسم الإطار الخارجي من الجهات الأربع بالكامل دون أي قصّ
                        using (Pen pen = new Pen(currentBorder, 2))
                        {
                            pen.Alignment = PenAlignment.Inset;
                            g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                        }
                    }
                    ReleaseDC(this.Handle, hdc);
                }
            }
        }
    }
}