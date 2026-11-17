using System;
using System.Drawing;
using System.Windows.Forms;

public class SlateTextBox : TextBox
{
    private const int WM_PAINT = 0xF;

    // نفس ألوان الكومبو بوكس والتايم بيكر بالحرف الواحد
    private Color normalBackColor = Color.FromArgb(30, 41, 59);       // الخلفية العادية (#1E293B)
    private Color focusBackColor = Color.FromArgb(40, 54, 77);        // خلفية أفتح عند الكتابة
    private Color normalBorderColor = Color.FromArgb(51, 65, 85);     // نفس حواف الكومبو بوكس والتايم بيكر (#334155)
    private Color focusBorderColor = Color.FromArgb(56, 189, 248);    // حواف زرقاء سماوية عند الكتابة (#38BDF8)

    public SlateTextBox()
    {
        this.BorderStyle = BorderStyle.FixedSingle; // نحتاجه فقط لحجز مساحة 1 بيكسل للإطار، وسنرسم فوقه
        this.BackColor = normalBackColor;
        this.ForeColor = Color.FromArgb(248, 250, 252);
        this.Font = new Font("Alexandria", 10f, FontStyle.Regular);
    }

    // عند الضغط داخل المربع للكتابة: تتغير الخلفية ويضيء الإطار
    protected override void OnEnter(EventArgs e)
    {
        base.OnEnter(e);
        this.BackColor = focusBackColor;
        this.Invalidate(); // أمر لإعادة رسم الإطار باللون الأزرق السماوي
    }

    // عند الخروج من المربع: يرجع للونه الهادئ
    protected override void OnLeave(EventArgs e)
    {
        base.OnLeave(e);
        this.BackColor = normalBackColor;
        this.Invalidate(); // أمر لإعادة رسم الإطار بلون الكومبو بوكس
    }

    // هنا نمسح الإطار الرمادي الافتراضي ونرسم إطارنا المطابق للكومبو بوكس
    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == WM_PAINT)
        {
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                // إذا كان المؤشر بالداخل يأخذ الأزرق السماوي، وإلا يأخذ نفس لون إطار الكومبو بوكس (51, 65, 85)
                Color currentBorder = this.Focused ? focusBorderColor : normalBorderColor;

                // سُمك 2 بيكسل مثل الكومبو بوكس تماماً ليغطي أي بياض أو رمادي من الويندوز
                using (Pen pen = new Pen(currentBorder, 2))
                {
                    g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                }
            }
        }
    }
}