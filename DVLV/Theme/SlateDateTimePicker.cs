using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class SlateDateTimePicker : DateTimePicker
{
    // ألوان ثيم (Midnight Slate)
    private Color normalBackColor = Color.FromArgb(30, 41, 59);       // خلفية داكنة (Slate 800)
    private Color headerBackColor = Color.FromArgb(71, 85, 105);      // لون الشريط العلوي وأيام الأسبوع (Slate 600)
    private Color textColor = Color.FromArgb(248, 250, 252);          // النص أبيض ثلجي
    private Color borderColor = Color.FromArgb(51, 65, 85);           // إطار الأداة نفسها
    private Color iconColor = Color.FromArgb(56, 189, 248);           // أزرق سماوي فاتح للأيقونات والإطار المنبثق (#38BDF8)
    private Color trailingTextColor = Color.FromArgb(100, 116, 139);  // أيام الشهر السابق/القادم

    private CalendarPaintHook calendarHook;
    private PopupBorderHook popupHook;

    // ثوابت ودوال نظام الويندوز
    private const int DTM_GETMONTHCAL = 0x1008;
    private const int MCM_GETMINREQRECT = 0x1009;
    private const int SWP_NOMOVE = 0x0002;
    private const int SWP_NOZORDER = 0x0004;
    private const int SWP_NOACTIVATE = 0x0010;

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT lParam);

    [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    public SlateDateTimePicker()
    {
        this.SetStyle(ControlStyles.UserPaint, true);
        this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        this.CalendarFont = new Font("Segoe UI", 10F, FontStyle.Regular);

        // تحديد ألوان التقويم المنبثق
        this.CalendarMonthBackground = normalBackColor;
        this.CalendarForeColor = textColor;
        this.CalendarTitleBackColor = headerBackColor;
        this.CalendarTitleForeColor = iconColor;
        this.CalendarTrailingForeColor = trailingTextColor;
    }

    protected override void OnDropDown(EventArgs eventargs)
    {
        base.OnDropDown(eventargs);

        IntPtr calendarHandle = SendMessage(this.Handle, DTM_GETMONTHCAL, 0, 0);

        if (calendarHandle != IntPtr.Zero)
        {
            // 1. إيقاف الثيم الافتراضي لتفعيل الألوان الداكنة
            SetWindowTheme(calendarHandle, "", "");

            // 2. ضبط الحجم الكامل للتقويم
            RECT rect = new RECT();
            SendMessage(calendarHandle, MCM_GETMINREQRECT, 0, ref rect);

            int width = (rect.Right - rect.Left) + 4;
            int height = (rect.Bottom - rect.Top) + 4;

            SetWindowPos(calendarHandle, IntPtr.Zero, 2, 2, width - 4, height - 4, SWP_NOZORDER | SWP_NOACTIVATE);

            IntPtr parentPopup = GetParent(calendarHandle);
            if (parentPopup != IntPtr.Zero)
            {
                SetWindowPos(parentPopup, IntPtr.Zero, 0, 0, width, height, SWP_NOMOVE | SWP_NOZORDER | SWP_NOACTIVATE);

                // ربط مراقب الرسم بالحاوية الخارجية لرسم الإطار الأزرق الفاتح فوق الإطار الأبيض
                if (popupHook == null) popupHook = new PopupBorderHook(this);
                popupHook.AssignHandle(parentPopup);
            }

            // ربط مراقب الرسم بالتقويم الداخلي لتلوين مربعات الأسهم البيضاء
            if (calendarHook == null) calendarHook = new CalendarPaintHook(this);
            calendarHook.AssignHandle(calendarHandle);
        }
    }

    protected override void OnCloseUp(EventArgs eventargs)
    {
        base.OnCloseUp(eventargs);
        if (calendarHook != null) calendarHook.ReleaseHandle();
        if (popupHook != null) popupHook.ReleaseHandle();
    }

    // --- كلاس داخلي 1: مسؤول عن تلوين أزرار الأسهم البيضاء ورسم الإطار الأزرق للتقويم ---
    private class CalendarPaintHook : NativeWindow
    {
        private SlateDateTimePicker owner;
        private const int WM_PAINT = 0x000F;

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        public CalendarPaintHook(SlateDateTimePicker owner)
        {
            this.owner = owner;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                RECT rect;
                GetClientRect(this.Handle, out rect);
                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    // أبعاد ومواقع المربعات البيضاء الخاصة بأسهم التنقل (يمين ويسار)
                    Rectangle leftBtn = new Rectangle(4, 4, 26, 22);
                    Rectangle rightBtn = new Rectangle(width - 30, 4, 26, 22);

                    // 1. طلاء مكان المربعات البيضاء بنفس لون الشريط العلوي لتختفي تماماً
                    using (SolidBrush bgBrush = new SolidBrush(owner.headerBackColor))
                    {
                        g.FillRectangle(bgBrush, leftBtn);
                        g.FillRectangle(bgBrush, rightBtn);
                    }

                    // 2. رسم خلفية أنيقة للأزرار بلون كحلي غامق مع إطار رفيع
                    Rectangle leftInner = new Rectangle(6, 5, 20, 18);
                    Rectangle rightInner = new Rectangle(width - 26, 5, 20, 18);

                    using (SolidBrush btnBrush = new SolidBrush(owner.normalBackColor))
                    using (Pen btnBorder = new Pen(owner.borderColor, 1))
                    {
                        g.FillRectangle(btnBrush, leftInner);
                        g.DrawRectangle(btnBorder, leftInner);

                        g.FillRectangle(btnBrush, rightInner);
                        g.DrawRectangle(btnBorder, rightInner);
                    }

                    // 3. رسم الأسهم باللون الأزرق الفاتح (السماوي)
                    using (SolidBrush arrowBrush = new SolidBrush(owner.iconColor))
                    {
                        // سهم اليسار (<)
                        Point[] leftArrow = new Point[]
                        {
                            new Point(leftInner.X + 12, leftInner.Y + 5),
                            new Point(leftInner.X + 12, leftInner.Y + 13),
                            new Point(leftInner.X + 6, leftInner.Y + 9)
                        };
                        g.FillPolygon(arrowBrush, leftArrow);

                        // سهم اليمين (>)
                        Point[] rightArrow = new Point[]
                        {
                            new Point(rightInner.X + 8, rightInner.Y + 5),
                            new Point(rightInner.X + 8, rightInner.Y + 13),
                            new Point(rightInner.X + 14, rightInner.Y + 9)
                        };
                        g.FillPolygon(arrowBrush, rightArrow);
                    }

                    // 4. رسم إطار أزرق فاتح حول التقويم من الداخل
                    using (Pen borderPen = new Pen(owner.iconColor, 2))
                    {
                        g.DrawRectangle(borderPen, 0, 0, width - 1, height - 1);
                    }
                }
            }
        }
    }

    // --- كلاس داخلي 2: مسؤول عن إخفاء الحواف البيضاء للحاوية المنبثقة وتلوينها بالأزرق الفاتح ---
    private class PopupBorderHook : NativeWindow
    {
        private SlateDateTimePicker owner;
        private const int WM_PAINT = 0x000F;
        private const int WM_NCPAINT = 0x0085;

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public PopupBorderHook(SlateDateTimePicker owner)
        {
            this.owner = owner;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT || m.Msg == WM_NCPAINT)
            {
                IntPtr hdc = GetWindowDC(this.Handle);
                if (hdc != IntPtr.Zero)
                {
                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        RECT rect;
                        GetWindowRect(this.Handle, out rect);
                        int width = rect.Right - rect.Left;
                        int height = rect.Bottom - rect.Top;

                        // رسم إطار أزرق سماوي فاتح يغطي الحافة البيضاء بالكامل
                        using (Pen borderPen = new Pen(owner.iconColor, 2))
                        {
                            g.DrawRectangle(borderPen, 1, 1, width - 2, height - 2);
                        }
                    }
                    ReleaseDC(this.Handle, hdc);
                }
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // 1. رسم الخلفية الداكنة للأداة
        using (SolidBrush backBrush = new SolidBrush(normalBackColor))
        {
            g.FillRectangle(backBrush, this.ClientRectangle);
        }

        // 2. رسم النص (التاريخ)
        using (SolidBrush textBrush = new SolidBrush(textColor))
        {
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;

            Rectangle textRect = new Rectangle(5, 0, this.Width - 30, this.Height);
            g.DrawString(this.Text, this.Font, textBrush, textRect, format);
        }

        // 3. رسم الإطار الخارجي
        using (Pen borderPen = new Pen(borderColor, 2))
        {
            g.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
        }

        // 4. رسم زر السهم
        int iconWidth = 20;
        Rectangle iconRect = new Rectangle(this.Width - iconWidth - 5, 0, iconWidth, this.Height);

        using (SolidBrush iconBrush = new SolidBrush(iconColor))
        {
            int arrowWidth = 10;
            int arrowHeight = 5;

            int x = iconRect.X + (iconRect.Width - arrowWidth) / 2;
            int y = iconRect.Y + (iconRect.Height - arrowHeight) / 2 + 1;

            Point[] arrowPoints = new Point[]
            {
                new Point(x, y),
                new Point(x + arrowWidth, y),
                new Point(x + (arrowWidth / 2), y + arrowHeight)
            };

            g.FillPolygon(iconBrush, arrowPoints);
        }
    }
}