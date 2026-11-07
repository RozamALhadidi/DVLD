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

namespace DVLV.People
{
    public partial class frmShowPersonDetails : Form
    {
        public frmShowPersonDetails(clsPerson Person)
        {
            InitializeComponent();
            uctrlPersonDetails1.ShowDetails(Person);
           
        }

       

    }
}
