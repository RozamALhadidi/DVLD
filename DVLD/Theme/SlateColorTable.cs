using System.Drawing;
using System.Windows.Forms;

namespace DVLD
{
    public class SlateColorTable : ProfessionalColorTable
    {
        // ألوان الثيم الخاص بك
        private Color backColor = Color.FromArgb(15, 23, 42);       // كحلي غامق (الخلفية)
        private Color hoverColor = Color.FromArgb(30, 41, 59);      // كحلي أفتح (عند التمرير بالماوس)
        private Color accentSkyBlue = Color.FromArgb(56, 189, 248); // أزرق سماوي (للحدود)
        private Color imageMarginColor = Color.FromArgb(22, 32, 50); // لون شريط الأيقونات على اليسار

        public override Color ToolStripDropDownBackground => backColor;

        // لون الشريط الجانبي الذي يحوي الأيقونات
        public override Color ImageMarginGradientBegin => imageMarginColor;
        public override Color ImageMarginGradientMiddle => imageMarginColor;
        public override Color ImageMarginGradientEnd => imageMarginColor;

        // لون الحدود الخارجية للقائمة
        public override Color MenuBorder => accentSkyBlue;

        // لون خلفية العنصر عند التحديد (Hover)
        public override Color MenuItemSelected => hoverColor;
        public override Color MenuItemBorder => hoverColor; // لتوحيد اللون بدون إطار مزعج عند التحديد
    }
}