using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DVLV 
{
    public class DarkPictureBox : PictureBox
    {
        public DarkPictureBox()
        {
         
            this.BackColor = AppTheme.FormBackColor; 
            this.SizeMode = PictureBoxSizeMode.Zoom;

            this.ErrorImage = null;
            this.InitialImage = null;
        }

        
        public void LoadImageSafe(string imagePath, Image defaultImage)
        {
           
            if (this.Image != null)
            {
                this.Image.Dispose();
                this.Image = null;
            }

            try
            {
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                
                    this.Image = Image.FromFile(imagePath);
                }
                else
                {
                    this.Image = defaultImage;
                }
            }
            catch
            {
               
                this.Image = defaultImage;
            }
        }
    }
}