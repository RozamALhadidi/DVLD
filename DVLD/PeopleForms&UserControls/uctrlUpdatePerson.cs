using DVLD.Properties;
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
        private Image _GenderImg = null;
        private bool _PersonImg = false;
        private string _PersonSourceImgPath = "";
        clsPerson Person;

        public uctrlUpdatePerson()
        {
            InitializeComponent();
            this.BackColor = AppTheme.FormBackColor;
            this.Size = AppTheme.UserFormsSize;



        }
        private void uctrlUpdatePerson_Load(object sender, EventArgs e)
        {
            this.BackColor = AppTheme.FormBackColor;
            this.Size = AppTheme.UserFormsSize;

            if (cbCountries.DataSource == null)
                _PrepareControls();
        }

        private void _PrepareControls()
        {

            DataTable dtCountries = clsCountries.GetAllCountries();

            cbCountries.DataSource = dtCountries;
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "countryid";

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            ofdPersonImage.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

        }


        private void _PutGenderImg(Image GenderImg)
        {
            if (GenderImg == null) return;
            Image coloredDefault = AppTheme.ChangeIconColor(GenderImg, ColorTranslator.FromHtml("#64748B"));
            Image finalResizedImage = SlatePictureBox.ResizeImage(coloredDefault, 100, 100);
            pbPersonImage.LoadImageSafe("", finalResizedImage);
        }
        
        private void _SelectPersonImage()
        {
            if(ofdPersonImage.ShowDialog()==DialogResult.OK)
            {
                _PersonSourceImgPath = ofdPersonImage.FileName;
                _PersonImg = true;
                pbPersonImage.LoadImageSafe(_PersonSourceImgPath, _GenderImg);
            }
        }
       
        public void LoadData(short id)
        {
            if (cbCountries.DataSource == null)
                _PrepareControls();

            Person = clsPerson.GetPersonByID(id);
            if (Person == null)
            {
                MessageBox.Show($"No Person with ID = {id}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
                lblID.Text = Person.PersonID.ToString();
                txtFname.Text = Person.FirstName;
                txtSName.Text = Person.SecondName;
                txtTName.Text = !string.IsNullOrEmpty(Person.ThirdName)?Person.ThirdName:"";
                txtLName.Text = Person.LastName;
                txtEmail.Text = !string.IsNullOrEmpty(Person.Email) ? Person.Email : "";
                txtAddress.Text = Person.Address;
                txtNationalNo.Text = !string.IsNullOrEmpty(Person.NationalityNo) ? Person.NationalityNo : "" ;
                txtPhone.Text = Person.Phone;
                dtpDateOfBirth.Value = Person.DateOfBirth;
                cbCountries.SelectedValue = Person.NationalityCountryID;

                _PersonImg = !string.IsNullOrEmpty(Person.ImagePath);
                _PersonSourceImgPath = "";

                if (Person.Gender == 0)
                {
                    rbMale.Checked=true;
                    _GenderImg = Resources.man_head;
                }
                else if (Person.Gender == 1)
                {
                    rbFemale.Checked = true;
                    _GenderImg = Resources.woman_head;                  
                }

                if(_PersonImg)
                {
                    Image coloredDefault = AppTheme.ChangeIconColor(_GenderImg, ColorTranslator.FromHtml("#64748B"));
                    Image finalResizedImage = SlatePictureBox.ResizeImage(coloredDefault, 100, 100);
                    pbPersonImage.LoadImageSafe(Person.ImagePath, finalResizedImage);
                }
                else
                {
                    _PutGenderImg(_GenderImg);
                }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            OnCloseClicked?.Invoke();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckDataBeforeSave())
            {
                return;
            }

            if(!_PersonImg)
            {
                Person.ImagePath = "";
            }
            else if (_PersonImg && !string.IsNullOrEmpty(_PersonSourceImgPath))
            {
                if (!Person.CopyandSaveImg(clsAppSettings.PeopleImagesFolder, _PersonSourceImgPath))
                {
                    MessageBox.Show("Error saving person image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
         
            Person.FirstName = txtFname.Text.Trim();
            Person.SecondName = txtSName.Text.Trim();
            Person.ThirdName = txtTName.Text.Trim();
            Person.LastName = txtLName.Text.Trim();
            Person.Email = txtEmail.Text.Trim();
            Person.Phone = txtPhone.Text.Trim();
            Person.Address = txtAddress.Text;
            Person.DateOfBirth = dtpDateOfBirth.Value;
            Person.NationalityNo = txtNationalNo.Text;
            Person.NationalityCountryID = (cbCountries.SelectedValue != null
                && short.TryParse(cbCountries.SelectedValue.ToString(), out short result))
            ? result : (short)-1;
            Person.Gender = rbMale.Checked ? (short)0 : (short)1;
            Person.ImagePath = !string.IsNullOrEmpty(Person.ImagePath) ? Person.ImagePath : "";

            if (Person.Save())
            {
                AfterSuccessfullSave();
            }
            else
            {
                MessageBox.Show("Error: Data IS NOT Saved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar=='+')
            {
                if (txtPhone.SelectionStart == 0 && !txtPhone.Text.Contains("+"))
                    return;
                else
                    e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked)
            {
                _GenderImg = Properties.Resources.woman_head;
                if (_PersonImg == false)
                    _PutGenderImg(_GenderImg);
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked)
            {
                _GenderImg = Properties.Resources.man_head;
                if (_PersonImg == false)
                    _PutGenderImg(_GenderImg);
            }
        }

        private void btnRemoveImg_Click(object sender, EventArgs e)
        {
            _PersonImg = false;
            _PersonSourceImgPath = "";
            _PutGenderImg(_GenderImg);
        }

        private void btnChangeImg_Click(object sender, EventArgs e)
        {
            _SelectPersonImage();
        }

        private void pbPersonImage_Click(object sender, EventArgs e)
        {
            _SelectPersonImage();
        }
        private bool CheckDataBeforeSave()
        {
            if (!clsPerson.IsValidName(txtFname.Text))
            {
                MessageBox.Show("Please enter a valid First Name!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFname.Focus();
                return false;
            }
            if (!clsPerson.IsValidName(txtSName.Text))
            {
                MessageBox.Show("Please enter a valid Second Name!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSName.Focus();
                return false;
            }

            if (!clsPerson.IsValidName(txtLName.Text))
            {
                MessageBox.Show("Please enter a valid Last Name!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLName.Focus();
                return false;
            }

            if (!rbFemale.Checked && !rbMale.Checked)
            {
                MessageBox.Show("Please select Gender!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rbMale.Focus();
                return false;
            }

            if (!clsPerson.IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid Email!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            if(!clsPerson.IsValidPhone(txtPhone.Text))
            {
                MessageBox.Show("Please enter a valid Phone Number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please enter an Address!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }

            return true;
        }
        private void AfterSuccessfullSave()
        {
            lblID.Text = Person.PersonID.ToString();
            btnChangeImg.Enabled = false;
            btnSave.Enabled = false;
            btnRemoveImg.Enabled = false;
            pbPersonImage.Enabled = false;
            txtFname.Enabled = false;
            txtSName.Enabled = false;
            txtTName.Enabled = false;
            txtLName.Enabled = false;
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtAddress.Enabled = false;
            dtpDateOfBirth.Enabled = false;
            txtNationalNo.Enabled = false;
            panel3.Enabled = false;
            cbCountries.Enabled = false;
            MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
