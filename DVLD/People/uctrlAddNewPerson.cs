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
using System.Text.RegularExpressions;
using System.IO;

namespace DVLD.People
{
    public partial class uctrlAddNewPerson : UserControl
    {
        public event Action onCloseCliked;

        clsPerson Person;

        public uctrlAddNewPerson()
        {
            InitializeComponent();
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }
        private bool CheckEmailBeforeSave(string email)
        {
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address!", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            return true;
        }
        private void uctrlAddNewPerson_Load(object sender, EventArgs e)
        {
            this.BackColor = AppTheme.FormBackColor;
            crownGroupBox2.BackColor = AppTheme.FormBackColor;
            DataTable dtCountries = clsCountries.GetAllCountries();
            cbCountry.DataSource = dtCountries;
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "countryid";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            onCloseCliked?.Invoke();
        }

        private void btnChangeImg_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!CheckEmailBeforeSave(txtEmail.Text))
            {
                return;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "");
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "Invalid Email Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked)
            {
                Image defaultImg = Properties.Resources.FemaleDefault;
                string imagePath = (Person != null) ? Person.ImagePath : "";
               // pbPerson.LoadImageSafe(imagePath, defaultImg);
            }

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if(rbMale.Checked)
            {
            Image defaultImg = Properties.Resources.MaleDefault;
            string imagePath = (Person != null) ? Person.ImagePath : "";
            //pbPerson.LoadImageSafe(imagePath, defaultImg);
            }

        }
    }
}
