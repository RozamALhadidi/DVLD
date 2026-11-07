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
        public uctrlPersonDetails()
        {
            InitializeComponent();
        }

        public void ShowDetails(clsPerson Person)
        {
            lblAddress.Text = Person.Address.ToString();
            lblCountry.Text = Person.NationalityCountryID.ToString();
            lblDateOfBirth.Text = Person.DateOfBirth.ToString();
            lblEmail.Text = Person.Email.ToString();
            lblGender.Text = Person.Gender.ToString();
            lblName.Text = Person.FirstName.ToString() + Person.LastName.ToString();
            lblNationalNo.Text = Person.NationalityNo.ToString();
            lblPersonID.Text = Person.PersonID.ToString();
            lblPhone.Text = Person.Phone.ToString();
            pbPerson.SizeMode = PictureBoxSizeMode.Zoom;

            if (!string.IsNullOrEmpty(Person.ImagePath) && System.IO.File.Exists(Person.ImagePath))
            {
                pbPerson.Load(Person.ImagePath);
            }
            else
            {
                pbPerson.Image = (Person.Gender == 0) ? Properties.Resources.MaleDefault : Properties.Resources.FemaleDefault;
            }
            pbPerson.Load(Person.ImagePath);

        }
    }
}

