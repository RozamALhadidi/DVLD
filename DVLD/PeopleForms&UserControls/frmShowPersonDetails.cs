using DVLD_BusinessLayers;
using ReaLTaiizor.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReaLTaiizor.Controls.HopeTabPage;
using ReaLTaiizor.Colors;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;

namespace DVLD.People
{
    public partial class frmShowPersonDetails :Form
    {
        public frmShowPersonDetails(short PersonID )
        {
            InitializeComponent();

            this.Size = AppTheme.UserFormsSize;

         uctrlPersonDetails1.OnCloseClicked += () => this.Close();

           

         uctrlPersonDetails1.ShowDetails(PersonID);

        }

        private void frmShowPersonDetails_Load(object sender, EventArgs e)
        {

            this.BackColor = AppTheme.FormBackColor;
        }

      
    }
}
