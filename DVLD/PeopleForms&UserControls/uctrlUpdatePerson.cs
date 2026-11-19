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

namespace DVLD
{
    public partial class uctrlUpdatePerson : UserControl
    {
        public event Action OnCloseClicked;
        clsPerson Person;

        public uctrlUpdatePerson()
        {
            InitializeComponent();
            this.BackColor = AppTheme.FormBackColor;


            DataTable dtCountries = clsCountries.GetAllCountries();

            cbCountries.DataSource = dtCountries;
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "countryid";
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            OnCloseClicked?.Invoke();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("ok");

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked)
            {
                string imagePath = (Person != null) ? Person.ImagePath : "";

                Image coloredDefault = AppTheme.ChangeIconColor(Properties.Resources.man_head, ColorTranslator.FromHtml("#64748B"));
                Image finalResizedImage = SlatePictureBox.ResizeImage(coloredDefault, 100, 100);

                pbPersonImage.LoadImageSafe(imagePath, finalResizedImage);
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked)
            {
                string imagePath = (Person != null) ? Person.ImagePath : "";

                Image coloredDefault = AppTheme.ChangeIconColor(Properties.Resources.woman_head, ColorTranslator.FromHtml("#64748B"));
                Image finalResizedImage = SlatePictureBox.ResizeImage(coloredDefault, 100, 100);

                pbPersonImage.LoadImageSafe(imagePath, finalResizedImage);
            }
        }

        private void btnRemoveImg_Click(object sender, EventArgs e)
        {

        }
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public void LoadData(short id)
        {
            Person = clsPerson.GetPersonByID(id);
            
            try
            {
                lblID.Text = Person.PersonID.ToString();
                txtFname.Text = Person.FirstName;
                txtSname.Text = Person.SecondName;
                txtTname.Text = Person.ThirdName;
                txtLname.Text = Person.LastName;
                txtEmail.Text = Person.Email;
                txtAddress.Text = Person.Address;
                txtNationalNo.Text = Person.NationalityNo;
                txtPhone.Text = Person.Phone;
                clsCountries country = clsCountries.FindCountry(Person.NationalityCountryID);
                cbCountries.SelectedIndex = country.CountryID;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("error" + ex.Message);
            }
        }
    }
}
