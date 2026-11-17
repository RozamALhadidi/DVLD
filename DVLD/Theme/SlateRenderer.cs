using DVLD;
using System.Drawing;
using System.Windows.Forms;

public class SlateRenderer : ToolStripProfessionalRenderer
{
    public SlateRenderer() : base(new SlateColorTable()) { }

    // التدخل بقوة لفرض لون النص على كل عناصر القائمة
    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        e.TextColor = Color.FromArgb(248, 250, 252);

        e.TextFormat &= ~TextFormatFlags.HidePrefix;

        // 3. السماح للرسام بطباعة النص الآن (سيطبعه مع الخط السفلي دائماً)
        base.OnRenderItemText(e);
    }
}