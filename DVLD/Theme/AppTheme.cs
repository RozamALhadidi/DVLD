using System.Drawing;

namespace DVLD
{
    public static class AppTheme
    {
        public static readonly Color FormBackColor = Color.FromArgb(15, 23, 42);

        public static readonly Color OuterGroupBoxColor = Color.White;

        public static readonly Color InnerGroupBoxColor = Color.FromArgb(60, 63, 65);

        public static readonly Color ButtonNormalColor = Color.FromArgb(40, 42, 44);

        public static readonly Color ButtonHoverColor = Color.FromArgb(75, 78, 82);

        public static readonly Color ButtonClickColor = Color.Black;

        public static readonly Color BorderNormalColor = Color.Black;

        public static readonly Color BorderHoverColor = Color.White;

        public static readonly Color BorderClickColor = Color.Black;

        public static readonly Size UserFormsSize = new Size(1070, 650);

        public static readonly Size MainForms = new Size(1800, 1200);

        public static readonly Color PrimaryTextColor = Color.FromArgb(224, 224, 244);

        public static readonly Color ControlBackColor = Color.FromArgb(47, 49, 54);

        public static readonly Color HeaderBackColor = Color.FromArgb(40, 42, 45);

        public static readonly Color AltRowBackColor = Color.FromArgb(53, 56, 58);

        public static Image ChangeIconColor(Image originalImage, Color newColor)
        {
            if (originalImage == null)
            {
                return null;
            }

            Bitmap bitmap = new Bitmap(originalImage);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color pixel = bitmap.GetPixel(i, j);
                    if (pixel.A > 0)
                    {
                        bitmap.SetPixel(i, j, Color.FromArgb(pixel.A, newColor.R, newColor.G, newColor.B));
                    }
                }
            }

            return bitmap;
        }
    }
}