using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Apartments
{
    public partial class F1113NorthSaintVrainSt : Form
    {
        public F1113NorthSaintVrainSt()
        {
            InitializeComponent();
        }

        private void BRent_Click(object sender, EventArgs e)
        {
            InformationForm iF1113NorthSaintVrainSt = new InformationForm("1113 North Saint Vrain St", ABC_Apartments.Properties.Resources._1113_North_Saint_Vrain_St, "E:\\Users\\Jag\\Documents\\ABC Apartements\\1113_North_Saint_Vrain_St.xml");
            iF1113NorthSaintVrainSt.Show();
        }
    }
}
