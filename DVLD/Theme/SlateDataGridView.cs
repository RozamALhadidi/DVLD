using System.Drawing;
using System.Windows.Forms;

namespace DVLD
{
    public class SlateDataGridView : DataGridView
    {
        public SlateDataGridView()
        {
            // 1. تفعيل الـ DoubleBuffer لمنع التقطيع عند تحريك الجدول
            this.DoubleBuffered = true;

            // 2. إعدادات المظهر العام والحدود
            this.BackgroundColor = Color.FromArgb(15, 23, 42); // خلفية الفراغ المتبقي تحت الصفوف (#0F172A)
            this.BorderStyle = BorderStyle.None;
            this.CellBorderStyle = DataGridViewCellBorderStyle.Single; // تفعيل حدود الخلايا بالطول والعرض
            this.GridColor = Color.FromArgb(56, 189, 248);             // لون خطوط الشبكة الأزرق السماوي
            this.RowHeadersVisible = false;                    // إخفاء العمود الرمادي الفارغ على اليسار
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // تحديد الصف بالكامل
            this.MultiSelect = false;
            this.ReadOnly = true;
            this.AllowUserToAddRows = false;
            this.AllowUserToResizeRows = false;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // وضع الاحتواء لمنع شريط التمرير الأفقي

            // 3. تنسيق الهيدر العلوي (رؤوس الأعمدة)
            this.EnableHeadersVisualStyles = false; // السر هنا لإلغاء اللون الأبيض الإجباري!
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single; // تفعيل حدود الهيدر بالطول والعرض
            this.ColumnHeadersHeight = 45;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42); // كحلي عميق
            this.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(56, 189, 248); // نص أزرق سماوي (#38BDF8)
            this.ColumnHeadersDefaultCellStyle.Font = new Font("Alexandria", 10F, FontStyle.Bold);
            this.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(15, 23, 42);
            this.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(56, 189, 248);
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // 4. تنسيق الصفوف والخلايا
            this.RowTemplate.Height = 38;
            this.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59); // كحلي نفس التيكست بوكس (#1E293B)
            this.DefaultCellStyle.ForeColor = Color.FromArgb(248, 250, 252); // أبيض ثلجي مريح للعين
            this.DefaultCellStyle.Font = new Font("Alexandria", 8F, FontStyle.Regular);
            this.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // لون الصف عند تحديده بالماوس
            this.DefaultCellStyle.SelectionBackColor = Color.FromArgb(56, 189, 248); // خلفية سماوية
            this.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);   // نص كحلي داكن وواضح

            // 5. تمييز الصفوف بالتناوب (صف غامق وصف أفتح قليلاً لسهولة القراءة)
            this.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(22, 32, 50);
            this.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(248, 250, 252);
            this.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(56, 189, 248);
            this.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        }
    }
}