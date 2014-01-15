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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void B1113NorthSaintVrainSt_Click(object sender, EventArgs e)
        {
            F1113NorthSaintVrainSt f1113NorthSaintVrainSt = new F1113NorthSaintVrainSt();
            f1113NorthSaintVrainSt.Show();
        }

        private void B605ERioRrandeAve_Click(object sender, EventArgs e)
        {
            F605ERioGrandeAve f605ERioRrandeAve = new F605ERioGrandeAve();
            f605ERioRrandeAve.Show();
        }

        private void B1118ERioGrandeAve_Click(object sender, EventArgs e)
        {
            F1118ERioGrandeAve f1118ERioGrandeAve = new F1118ERioGrandeAve();
            f1118ERioGrandeAve.Show();
        }

        private void B811ArizonaAve_Click(object sender, EventArgs e)
        {
            F811ArizonaAve f811ArizonaAve = new F811ArizonaAve();
            f811ArizonaAve.Show();
        }
    }
}
