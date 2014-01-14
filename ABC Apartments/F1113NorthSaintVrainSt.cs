using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Apartments{
    public partial class F1113NorthSaintVrainSt : Form{
        //Local Varables
        private string tBFirstNameDefaultValue = "Primer Nombre";
        private string tBLastNameDefaultValue = "Apellido";
        private string tBTenantsDefaultValue = "Nombre Completo";

        //Constructor
        public F1113NorthSaintVrainSt(){
            InitializeComponent();
            if (TBFirstName.Text == ""){
                TBFirstName.ForeColor = Color.Gray;
                TBFirstName.Text = tBFirstNameDefaultValue;
            }
            
            if (TBLastName.Text == "")
            {
                TBLastName.ForeColor = Color.Gray;
                TBLastName.Text = tBLastNameDefaultValue;
            }

            if (TBTenants1.Text == "")
            {
                TBTenants1.ForeColor = Color.Gray;
                TBTenants1.Text = tBTenantsDefaultValue;
            }
            if (TBTenants2.Text == "")
            {
                TBTenants2.ForeColor = Color.Gray;
                TBTenants2.Text = tBTenantsDefaultValue;
            }
            if (TBTenants3.Text == "")
            {
                TBTenants3.ForeColor = Color.Gray;
                TBTenants3.Text = tBTenantsDefaultValue;
            }
            if (TBTenants4.Text == "")
            {
                TBTenants4.ForeColor = Color.Gray;
                TBTenants4.Text = tBTenantsDefaultValue;
            }
            if (TBTenants5.Text == "")
            {
                TBTenants5.ForeColor = Color.Gray;
                TBTenants5.Text = tBTenantsDefaultValue;
            }
            if (TBTenants6.Text == "")
            {
                TBTenants6.ForeColor = Color.Gray;
                TBTenants6.Text = tBTenantsDefaultValue;
            }
        }

        //Responsible Party
        private void TBFirstName_Enter(object sender, EventArgs e)
        {
            if (TBFirstName.Text == "" || TBFirstName.Text == tBFirstNameDefaultValue)
            {
                TBFirstName.Clear();
                TBFirstName.ForeColor = Color.Black;
            }
        }

        private void TBFirstName_Leave(object sender, EventArgs e)
        {
            if (TBFirstName.Text == "")
            {
                TBFirstName.ForeColor = Color.Gray;
                TBFirstName.Text = tBFirstNameDefaultValue;
            }
        }

        private void TBLastName_Enter(object sender, EventArgs e)
        {
            if (TBLastName.Text == "" || TBLastName.Text == tBLastNameDefaultValue)
            {
                TBLastName.Clear();
                TBLastName.ForeColor = Color.Black;
            }
        }

        private void TBLastName_Leave(object sender, EventArgs e)
        {
            if (TBLastName.Text == "")
            {
                TBLastName.ForeColor = Color.Gray;
                TBLastName.Text = tBLastNameDefaultValue;
            }
        }

        //Additional Tenets
        private void TBTenants1_Enter(object sender, EventArgs e)
        {
            if (TBTenants1.Text == "" || TBTenants1.Text == tBTenantsDefaultValue)
            {
                TBTenants1.Clear();
                TBTenants1.ForeColor = Color.Black;
            }
        }
        private void TBTenants1_Leave(object sender, EventArgs e)
        {
            if (TBTenants1.Text == "")
            {
                TBTenants1.ForeColor = Color.Gray;
                TBTenants1.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants2_Enter(object sender, EventArgs e)
        {
            if (TBTenants2.Text == "" || TBTenants2.Text == tBTenantsDefaultValue)
            {
                TBTenants2.Clear();
                TBTenants2.ForeColor = Color.Black;
            }
        }
        private void TBTenants2_Leave(object sender, EventArgs e)
        {
            if (TBTenants2.Text == "")
            {
                TBTenants2.ForeColor = Color.Gray;
                TBTenants2.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants3_Enter(object sender, EventArgs e)
        {
            if (TBTenants3.Text == "" || TBTenants3.Text == tBTenantsDefaultValue)
            {
                TBTenants3.Clear();
                TBTenants3.ForeColor = Color.Black;
            }
        }
        private void TBTenants3_Leave(object sender, EventArgs e)
        {
            if (TBTenants3.Text == "")
            {
                TBTenants3.ForeColor = Color.Gray;
                TBTenants3.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants4_Enter(object sender, EventArgs e)
        {
            if (TBTenants4.Text == "" || TBTenants4.Text == tBTenantsDefaultValue)
            {
                TBTenants4.Clear();
                TBTenants4.ForeColor = Color.Black;
            }
        }
        private void TBTenants4_Leave(object sender, EventArgs e)
        {
            if (TBTenants4.Text == "")
            {
                TBTenants4.ForeColor = Color.Gray;
                TBTenants4.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants5_Enter(object sender, EventArgs e)
        {
            if (TBTenants5.Text == "" || TBTenants5.Text == tBTenantsDefaultValue)
            {
                TBTenants5.Clear();
                TBTenants5.ForeColor = Color.Black;
            }
        }
        private void TBTenants5_Leave(object sender, EventArgs e)
        {
            if (TBTenants5.Text == "")
            {
                TBTenants5.ForeColor = Color.Gray;
                TBTenants5.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants6_Enter(object sender, EventArgs e)
        {
            if (TBTenants6.Text == "" || TBTenants6.Text == tBTenantsDefaultValue)
            {
                TBTenants6.Clear();
                TBTenants6.ForeColor = Color.Black;
            }
        }
        private void TBTenants6_Leave(object sender, EventArgs e)
        {
            if (TBTenants6.Text == "")
            {
                TBTenants6.ForeColor = Color.Gray;
                TBTenants6.Text = tBTenantsDefaultValue;
            }
        }

        //Utilites
        private void CBGas_CheckedChanged(object sender, EventArgs e)
        {
            if(CBGas.Checked){
                TBGas.Enabled = false;
            }
            else
            {
                TBGas.Enabled = true;
            }
        }

        private void CBElectric_CheckedChanged(object sender, EventArgs e)
        {
            if (CBElectric.Checked)
            {
                TBElectric.Enabled = false;
            }
            else
            {
                TBElectric.Enabled = true;
            }
        }

        private void CBWatter_CheckedChanged(object sender, EventArgs e)
        {
            if (CBWatter.Checked)
            {
                TBWatter.Enabled = false;
            }
            else
            {
                TBWatter.Enabled = true;
            }
        }

        private void CBTrash_CheckedChanged(object sender, EventArgs e)
        {
            if (CBTrash.Checked)
            {
                TBTrash.Enabled = false;
            }
            else
            {
                TBTrash.Enabled = true;
            }
        }

        
    }
}
