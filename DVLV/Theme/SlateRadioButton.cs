using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class SlateRadioButton : RadioButton
{
    public SlateRadioButton()
    {
        this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        this.BackColor = Color.Transparent; 
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.Clear(this.BackColor);

        Color circleBorderColor = Color.FromArgb(51, 65, 85);   
        Color checkedColor = Color.FromArgb(56, 189, 248);      
        Color textColor = Color.FromArgb(241, 245, 249);        

        int circleSize = 14; 
        Rectangle rect = new Rectangle(0, (this.Height - circleSize) / 2, circleSize, circleSize);

        using (Pen pen = new Pen(this.Checked ? checkedColor : circleBorderColor, 2f))
        {
            e.Graphics.DrawEllipse(pen, rect);
        }

        if (this.Checked)
        {
            Rectangle innerRect = new Rectangle(rect.X + 3, rect.Y + 3, rect.Width - 6, rect.Height - 6);
            using (SolidBrush brush = new SolidBrush(checkedColor))
            {
                e.Graphics.FillEllipse(brush, innerRect);
            }
        }

        using (SolidBrush textBrush = new SolidBrush(textColor))
        {
            PointF textLocation = new PointF(20, (this.Height - this.Font.Height) / 2);
            e.Graphics.DrawString(this.Text, this.Font, textBrush, textLocation);
        }
    }
}