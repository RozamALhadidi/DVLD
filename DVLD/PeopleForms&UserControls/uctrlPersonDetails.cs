using DVLD_BusinessLayers;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class uctrlPersonDetails : UserControl
    {
        public event Action OnCloseClicked;
        short _PersonID = -1;
        clsPerson Person;
        public uctrlPersonDetails()
        {

            InitializeComponent();

        }

        public void ShowDetails(short PersonID)
        {
            _GetPersonData(PersonID);

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            OnCloseClicked?.Invoke();
        }

        private void btnEditPerson_Click(object sender, EventArgs e)
        {
            if (_PersonID == -1) return;
            using (frmUpdatePersonDetails frm = new frmUpdatePersonDetails(_PersonID))
            {
                frm.ShowDialog();
            }
            _GetPersonData(_PersonID);
        }
        private void _GetPersonData(short PersonID)
        {
            Person = clsPerson.GetPersonByID(PersonID);
            if (Person == null)
            {
                _PersonID = -1;
                MessageBox.Show($"No Person with ID = {PersonID}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _PersonID = Person.PersonID;

            lblAddress.Text = Person.Address ?? "";

            clsCountries Country = clsCountries.FindCountry(Person.NationalityCountryID);
            lblCountry.Text = (Country != null) ? Country.CountryName.ToString() : "Unknown";

            lblDateofBirth.Text = Person.DateOfBirth.ToShortDateString();
            lblEmail.Text = Person.Email ?? "";
            lblGender.Text = (Person.Gender == 0) ? "Male" : "Female";
            lblPhone.Text = Person.Phone.ToString();
            lblNatioN.Text = Person.NationalityNo ?? "";

            lblFullName.Text = string.Join(" ", new[] { Person.FirstName, Person.SecondName, Person.ThirdName, Person.LastName }
            .Where(s => !string.IsNullOrWhiteSpace(s)));

            lblID.Text = Person.PersonID.ToString();

            Image defaultImg = (Person.Gender == 0) ? Properties.Resources.man_head : Properties.Resources.woman_head;
            Image coloredDefault = AppTheme.ChangeIconColor(defaultImg, ColorTranslator.FromHtml("#64748B"));
            Image finalResizedImage = SlatePictureBox.ResizeImage(coloredDefault, 100, 100);
            pbPerson.LoadImageSafe(Person.ImagePath, finalResizedImage);
        }
    }
}

