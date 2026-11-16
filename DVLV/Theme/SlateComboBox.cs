using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices; // إضافة ضرورية للاتصال بنظام الويندوز
using System.Windows.Forms;

public class SlateComboBox : ComboBox
{
    private const int WM_PAINT = 0xF;
    private int buttonWidth = SystemInformation.VerticalScrollBarWidth;

    // استدعاء مكتبة الويندوز لإيقاف الوميض وتأثيرات الماوس الافتراضية
    [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

    public SlateComboBox()
    {
        this.DropDownStyle = ComboBoxStyle.DropDownList;
        this.FlatStyle = FlatStyle.Flat;
        this.BackColor = Color.FromArgb(30, 41, 59);
        this.ForeColor = Color.FromArgb(248, 250, 252);
    }

    // بمجرد أن يتم إنشاء الكومبو بوكس على الشاشة، نقوم بفصل تأثيرات الويندوز عنه
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        SetWindowTheme(this.Handle, "", ""); // هذا السطر يقتل الوميض الأبيض نهائياً
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == WM_PAINT)
        {
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                // 1. رسم الإطار الخارجي
                Color borderColor = Color.FromArgb(51, 65, 85);
                using (Pen pen = new Pen(borderColor, 2))
                {
                    g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                }

                // 2. إعادة رسم مساحة الزر بالكامل (نفس كودك السابق)
                Rectangle buttonRect = new Rectangle(this.Width - buttonWidth - 1, 1, buttonWidth, this.Height - 2);

                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    g.FillRectangle(brush, buttonRect);
                }

                // 3. رسم السهم
                Color arrowColor = Color.FromArgb(56, 189, 248);
                using (SolidBrush arrowBrush = new SolidBrush(arrowColor))
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
            }
        }
    }
}