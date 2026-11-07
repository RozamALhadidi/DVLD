using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayers;
using DVLV.People;

namespace DVLD
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

    

        private void materialButton1_Click(object sender, EventArgs e)
        {
            short.TryParse(tb1.Text, out short id);

            clsPerson Person = clsPerson.GetUserByID(id);
            

            if (Person != null)
            {
                Form personfrm = new frmShowPersonDetails(Person);
                personfrm.ShowDialog();
                tb2.Text = Person.FirstName;
            }
            else
                tb2.Text = "name is not found";
        }
    }
}
