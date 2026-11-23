using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class SlatePanel : Panel
{
    private Color _borderColor = Color.FromArgb(56, 189, 248);
    private int _borderThickness = 2;

    [Category("Slate Appearance")]
    public Color BorderColor
    {
        get => _borderColor;
        set { _borderColor = value; this.Invalidate(); }
    }

    [Category("Slate Appearance")]
    public int BorderThickness
    {
        get => _borderThickness;
        set { _borderThickness = value; this.Invalidate(); }
    }

    public SlatePanel()
    {
        this.BackColor = Color.FromArgb(15, 23, 42);
        this.BorderStyle = BorderStyle.None;
        this.ResizeRedraw = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (_borderThickness > 0)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                this.ClientRectangle,
                _borderColor, _borderThickness, ButtonBorderStyle.Solid,
                _borderColor, _borderThickness, ButtonBorderStyle.Solid,
                _borderColor, _borderThickness, ButtonBorderStyle.Solid,
                _borderColor, _borderThickness, ButtonBorderStyle.Solid
            );
        }
    }
}