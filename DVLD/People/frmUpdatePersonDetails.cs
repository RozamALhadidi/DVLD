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
    public partial class frmUpdatePersonDetails : Form
    {
        clsPerson Person;
        public frmUpdatePersonDetails(short PersonID)
        {
            InitializeComponent();
            this.Size = AppTheme.UserFormsSize;
            uctrlUpdatePerson1.OnCloseClicked += () => this.Close();

           
           
            uctrlUpdatePerson1.LoadData(PersonID);
           

            

        }

        public frmUpdatePersonDetails()
        {
            InitializeComponent();
            this.Size = AppTheme.UserFormsSize;
            uctrlUpdatePerson1.OnCloseClicked += () => this.Close();
        }


        private void uctrlUpdatePerson1_Load(object sender, EventArgs e)
        {
            
        }
       
    }
}
