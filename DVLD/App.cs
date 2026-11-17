using DVLD_BusinessLayers;
using DVLD;
using DVLD.People;
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
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            short.TryParse(tb1.Text, out short id);

            clsPerson Person = clsPerson.GetPersonByID(id);



            if (Person != null)
            {
                Form personfrm = new frmShowPersonDetails(id);
                personfrm.ShowDialog();
                tb2.Text = Person.FirstName;
            }
            else
                tb2.Text = "name is not found";
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {
            Form frm = new frmUpdatePersonDetails();
            frm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = AppTheme.FormBackColor;
        }

        private void darkButton2_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddNewPerson();
            frm.Show();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManagePeople();
            frm.Show();
        }
    }
}
