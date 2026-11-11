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

namespace DVLV.People
{
    public partial class uctrlPersonDetails : UserControl
    {
        public event Action OnCloseClicked;
        short _PersonID = -1;
        public uctrlPersonDetails()
        {

            InitializeComponent();

        }

        public void ShowDetails(clsPerson Person)
        {
            lblAddress.Text = Person.Address.ToString();

            clsCountries Country = clsCountries.FindCountry(Person.NationalityCountryID);
            lblCountry.Text = (Country != null) ? Country.CountryName.ToString() : "Unknown";
         

            lblDateOfBirth.Text = Person.DateOfBirth.ToString();
            lblEmail.Text = Person.Email.ToString();
            lblGender.Text = (Person.Gender == 0) ? "Male" : "Female";
            lblName.Text = Person.FirstName.ToString() + " " + Person.SecondName.ToString() +
                " " + Person.ThirdName.ToString() + " " + Person.LastName.ToString();
            lblNationalNo.Text = Person.NationalityNo.ToString();
            lblPersonID.Text = Person.PersonID.ToString();
            lblPhone.Text = Person.Phone.ToString();
            pbPerson.SizeMode = PictureBoxSizeMode.Zoom;

            _PersonID = Person.PersonID;

            Image defaultImg = (Person.Gender == 0) ? Properties.Resources.MaleDefault : Properties.Resources.FemaleDefault;
            pbPerson.LoadImageSafe(Person.ImagePath, defaultImg);

         

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            OnCloseClicked?.Invoke();
        }

        private void btnEditInfo_Click(object sender, EventArgs e)
        {
            Form frm = new frmUpdatePersonDetails(_PersonID);
            frm.ShowDialog();
        }
    }
}

