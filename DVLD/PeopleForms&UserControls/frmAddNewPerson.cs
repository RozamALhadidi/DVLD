using DVLD_BusinessLayers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmAddNewPerson : Form
    {
        clsPerson person;

        public frmAddNewPerson()
        {
            InitializeComponent();
            this.Size = AppTheme.UserFormsSize;
            uctrlAddNewPerson1.onCloseCliked += () => this.Close();
        }

        private void frmAddNewPerson_Load(object sender, EventArgs e)
        {
            this.BackColor = AppTheme.FormBackColor;
        }

    }
}
