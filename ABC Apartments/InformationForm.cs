using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ABC_Apartments{
    public partial class InformationForm : Form{
        //Local Varables
        private string tBFirstNameDefaultValue = "Primer Nombre";
        private string tBMiddleInitialDefaultValue = "In";
        private string tBLastNameDefaultValue = "Apellido";
        private string tBTenantsDefaultValue = "Nombre Completo";
        private string pictureIDLocation = "";
        private string xmlSaveFileLocation;
        private readonly string xmlSkeleton = 
            "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + "\n" +
                "<Apartment>" + "\n" +
                  "<Address></Address>" + "\n" +
                  "<Date>" + "\n" +
                    "<Date></Date>" + "\n" +
                    "<Tenants>" + "\n" +
                      "<Main_Tenant>" + "\n" +
                        "<First_Name></First_Name>" + "\n" +
                        "<Middle_Initial></Middle_Initial>" + "\n" +
                        "<Last_Name></Last_Name>" + "\n" +
                        "<Phone_Number></Phone_Number>" + "\n" +
                        "<Ocupation></Ocupation>" + "\n" +
                        "<DOB></DOB>" + "\n" +
                        "<ID_Location></ID_Location>" + "\n" +
                      "</Main_Tenant>" + "\n" +
                      "<Other_Tenants>" + "\n" +
                        "<Tenant_2>" + "\n" +
                          "<Name></Name>" + "\n" +
                          "<Phone_Number></Phone_Number>" + "\n" +
                        "</Tenant_2>" + "\n" +
                        "<Tenant_3>" + "\n" +
                          "<Name></Name>" + "\n" +
                          "<Phone_Number></Phone_Number>" + "\n" +
                        "</Tenant_3>" + "\n" +
                        "<Tenant_4>" + "\n" +
                          "<Name></Name>" + "\n" +
                          "<Phone_Number></Phone_Number>" + "\n" +
                        "</Tenant_4>" + "\n" +
                        "<Tenant_5>" + "\n" +
                          "<Name></Name>" + "\n" +
                          "<Phone_Number></Phone_Number>" + "\n" +
                        "</Tenant_5>" + "\n" +
                        "<Tenant_6>" + "\n" +
                          "<Name></Name>" + "\n" +
                          "<Phone_Number></Phone_Number>" + "\n" +
                        "</Tenant_6>" + "\n" +
                      "</Other_Tenants>" + "\n" +
                      "<Starting_Date></Starting_Date>" + "\n" +
                      "<Ending_Date></Ending_Date>" + "\n" +
                      "<Tenants_Notes></Tenants_Notes>" + "\n" +
                    "</Tenants>" + "\n" +
                    "<Payments>" + "\n" +
                      "<Deposit_Payment_Amount></Deposit_Payment_Amount>" + "\n" +
                      "<Deposit_Payment_Type></Deposit_Payment_Type>" + "\n" +
                      "<Deposit_Payment_Date></Deposit_Payment_Date>" + "\n" +
                      "<Rent_Payment_Amount></Rent_Payment_Amount>" + "\n" +
                      "<Rent_Payment_Type></Rent_Payment_Type>" + "\n" +
                      "<Rent_Paymnet_Date></Rent_Paymnet_Date>" + "\n" +
                      "<Payment_Notes></Payment_Notes>" + "\n" +
                    "</Payments>" + "\n" +
                    "<Utilities>" + "\n" +
                      "<Gas_We_Pay></Gas_We_Pay>" + "\n" +
                      "<Gas_Payment></Gas_Payment>" + "\n" +
                      "<Electric_We_Pay></Electric_We_Pay>" + "\n" +
                      "<Electric_Payment></Electric_Payment>" + "\n" +
                      "<Water_We_Pay></Water_We_Pay>" + "\n" +
                      "<Water_Payment></Water_Payment>" + "\n" +
                      "<Trash_We_Pay></Trash_We_Pay>" + "\n" +
                      "<Trash_Payment></Trash_Payment>" + "\n" +
                      "<Utilities_Notes></Utilities_Notes>" + "\n" +
                    "</Utilities>" + "\n" +
                    "<Notes>" + "\n" +
                      "<Other_Monthly_Expencess></Other_Monthly_Expencess>" + "\n" +
                      "<Additional_Notes></Additional_Notes>" + "\n" +
                    "</Notes>" + "\n" +
                  "</Date>" + "\n" +
                "</Apartment>";
        //Constructor
        public InformationForm(string title, Image image, string xmlSaveFileLocation){
            InitializeComponent();
            this.xmlSaveFileLocation = xmlSaveFileLocation;
            this.Text = title;
            PBApartmentImage.Image = image;

            populate();
            testDefaults();
        }

        private void populate(){
            if (!System.IO.File.Exists(xmlSaveFileLocation))
            {
                XmlTextWriter writer = new XmlTextWriter(xmlSaveFileLocation, null);
                XmlDocument temp = new XmlDocument();
                temp.LoadXml(xmlSkeleton);
                temp.Save(writer);
                writer.Close();
            }
            //Instanciate the XMLDocument object.
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlSaveFileLocation);

            //Main Tenant
            TBFirstName.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/First_Name").InnerText;
            TBMiddleInitial.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/Middle_Initial").InnerText;
            TBLastName.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/Last_Name").InnerText;
            TBPhoneNumber.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/Phone_Number").InnerText;
            TBOcupation.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/Ocupation").InnerText;

            if (doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/DOB").InnerText != "")
            {
                DTPDOB.Value = DateTime.Parse(doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/DOB").InnerText);
            }
            

            //Picture ID
            pictureIDLocation = doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/ID_Location").InnerText;
            if (pictureIDLocation != "")
            {
                PBPictureID.Image = Image.FromFile(pictureIDLocation);
            }
            //Other Tenants
            TBTenantsName1.Text = TBFirstName.Text + " " + TBMiddleInitial.Text + ". " + TBLastName.Text;
            TBTenantsPhoneNumber1.Text = TBPhoneNumber.Text;
            //Tenant 2
            TBTenantsName2.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_2/Name").InnerText;
            TBTenantsPhoneNumber2.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_2/Phone_Number").InnerText;
            //Tenant 3
            TBTenantsName3.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_3/Name").InnerText;
            TBTenantsPhoneNumber3.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_3/Phone_Number").InnerText;
            //Tenant 4
            TBTenantsName4.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_4/Name").InnerText;
            TBTenantsPhoneNumber4.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_4/Phone_Number").InnerText;
            //Tenant 5
            TBTenantsName5.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_5/Name").InnerText;
            TBTenantsPhoneNumber5.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_5/Phone_Number").InnerText;
            //Tenant 6
            TBTenantsName6.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_6/Name").InnerText;
            TBTenantsPhoneNumber6.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_6/Phone_Number").InnerText;

            if (doc.SelectSingleNode("/Apartment/Date/Tenants/Starting_Date").InnerText != "")
            {
                DTPStartingDay.Value = DateTime.Parse(doc.SelectSingleNode("/Apartment/Date/Tenants/Starting_Date").InnerText);
            }
            if (doc.SelectSingleNode("/Apartment/Date/Tenants/Ending_Date").InnerText != "")
            {
                DTPEndingDay.Value = DateTime.Parse(doc.SelectSingleNode("/Apartment/Date/Tenants/Ending_Date").InnerText);
            }
            RTBTenantsNotes.Text = doc.SelectSingleNode("/Apartment/Date/Tenants/Tenants_Notes").InnerText;

            //Payments
            //Deposit
            TBDepositAmount.Text = doc.SelectSingleNode("/Apartment/Date/Payments/Deposit_Payment_Amount").InnerText;
            CBDepositPaymentType.SelectedItem = doc.SelectSingleNode("/Apartment/Date/Payments/Deposit_Payment_Type").InnerText;
            if (doc.SelectSingleNode("/Apartment/Date/Payments/Deposit_Payment_Date").InnerText != "")
            {
                DTPRentPaymentDate.Value = DateTime.Parse(doc.SelectSingleNode("/Apartment/Date/Payments/Deposit_Payment_Date").InnerText);
            }
            //Rent
            TBRentAmount.Text = doc.SelectSingleNode("/Apartment/Date/Payments/Rent_Payment_Amount").InnerText;
            CBRentPaymentType.SelectedItem = doc.SelectSingleNode("/Apartment/Date/Payments/Rent_Payment_Type").InnerText;
            TBRentPaymentDay.Text = doc.SelectSingleNode("/Apartment/Date/Payments/Rent_Paymnet_Date").InnerText;

            //Notes
            RTBPaymentNotes.Text = doc.SelectSingleNode("/Apartment/Date/Payments/Payment_Notes").InnerText;

            //Utilitties
            //Gas
            try
            {
                CBGas.Checked = Boolean.Parse(doc.SelectSingleNode("/Apartment/Date/Utilities/Gas_We_Pay").InnerText);
            }
            catch(Exception){
                CBGas.Checked = false;
            }
            TBGas.Text = doc.SelectSingleNode("/Apartment/Date/Utilities/Gas_Payment").InnerText;
            //Electric
            try
            {
                CBElectric.Checked = Boolean.Parse(doc.SelectSingleNode("/Apartment/Date/Utilities/Electric_We_Pay").InnerText);
            }
            catch (Exception)
            {
                CBElectric.Checked = false;
            }
            TBElectric.Text = doc.SelectSingleNode("/Apartment/Date/Utilities/Electric_Payment").InnerText;
            //Watter
            try
            {
                CBWatter.Checked = Boolean.Parse(doc.SelectSingleNode("/Apartment/Date/Utilities/Water_We_Pay").InnerText);
            }
            catch (Exception)
            {
                CBWatter.Checked = false;
            }
            TBWatter.Text = doc.SelectSingleNode("/Apartment/Date/Utilities/Water_Payment").InnerText;
            //Trash
            try
            {
                CBTrash.Checked = Boolean.Parse(doc.SelectSingleNode("/Apartment/Date/Utilities/Trash_We_Pay").InnerText);
            }
            catch (Exception)
            {
                CBTrash.Checked = false;
            }
            TBTrash.Text = doc.SelectSingleNode("/Apartment/Date/Utilities/Trash_Payment").InnerText;
            //Notes
            RTBUtilitiesNotes.Text = doc.SelectSingleNode("/Apartment/Date/Utilities/Utilities_Notes").InnerText;

            //Monthly Notes
            RTBOtherMonthlyExpencess.Text = doc.SelectSingleNode("/Apartment/Date/Notes/Other_Monthly_Expencess").InnerText;
            RTBAdditionalNotes.Text = doc.SelectSingleNode("/Apartment/Date/Notes/Additional_Notes").InnerText;
        }

        private void testDefaults(){
            if (PBPictureID.Image != null)
            {
                BPictureID.Visible = false;
            }
            if (TBFirstName.Text == "" || TBFirstName.Text == tBFirstNameDefaultValue)
            {
                TBFirstName.ForeColor = Color.Gray;
                TBFirstName.Text = tBFirstNameDefaultValue;
            }
            if (TBMiddleInitial.Text == "" || TBMiddleInitial.Text == tBMiddleInitialDefaultValue)
            {
                TBMiddleInitial.ForeColor = Color.Gray;
                TBMiddleInitial.Text = tBMiddleInitialDefaultValue;
            }
            if (TBLastName.Text == "" || TBLastName.Text == tBLastNameDefaultValue)
            {
                TBLastName.ForeColor = Color.Gray;
                TBLastName.Text = tBLastNameDefaultValue;
            }

            if (TBTenantsName2.Text == "" || TBTenantsName2.Text == tBTenantsDefaultValue)
            {
                TBTenantsName2.ForeColor = Color.Gray;
                TBTenantsName2.Text = tBTenantsDefaultValue;
            }
            if (TBTenantsName3.Text == "" || TBTenantsName3.Text == tBTenantsDefaultValue)
            {
                TBTenantsName3.ForeColor = Color.Gray;
                TBTenantsName3.Text = tBTenantsDefaultValue;
            }
            if (TBTenantsName4.Text == "" || TBTenantsName4.Text == tBTenantsDefaultValue)
            {
                TBTenantsName4.ForeColor = Color.Gray;
                TBTenantsName4.Text = tBTenantsDefaultValue;
            }
            if (TBTenantsName5.Text == "" || TBTenantsName5.Text == tBTenantsDefaultValue)
            {
                TBTenantsName5.ForeColor = Color.Gray;
                TBTenantsName5.Text = tBTenantsDefaultValue;
            }
            if (TBTenantsName6.Text == "" || TBTenantsName6.Text == tBTenantsDefaultValue)
            {
                TBTenantsName6.ForeColor = Color.Gray;
                TBTenantsName6.Text = tBTenantsDefaultValue;
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
            else
            {
                updateFristTenantsName();
            }
        }

        private void TBMiddleInitial_Enter(object sender, EventArgs e)
        {
            if (TBMiddleInitial.Text == "" || TBMiddleInitial.Text == tBMiddleInitialDefaultValue)
            {
                TBMiddleInitial.Clear();
                TBMiddleInitial.ForeColor = Color.Black;
            }
        }
        private void TBMiddleInitial_Leave(object sender, EventArgs e)
        {
            if (TBMiddleInitial.Text == "")
            {
                TBMiddleInitial.ForeColor = Color.Gray;
                TBMiddleInitial.Text = tBMiddleInitialDefaultValue;
            }
            else
            {
                updateFristTenantsName();
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
            else
            {
                updateFristTenantsName();
            }
        }

        private void updateFristTenantsName(){
            string firstName = "";
            string middleInital = "";
            string lastName = "";

            if (TBFirstName.Text != tBFirstNameDefaultValue)
            {
                firstName = TBFirstName.Text;
            }
            if (TBMiddleInitial.Text != tBMiddleInitialDefaultValue){
                middleInital = TBMiddleInitial.Text + ".";
            }
            if (TBLastName.Text != tBLastNameDefaultValue){
                lastName = TBLastName.Text;
            }

            TBTenantsName1.Text = firstName + " " + middleInital + " " + lastName;
        }

        //Additional Tenets
        private void TBTenants1_Enter(object sender, EventArgs e)
        {
            if (TBTenantsName1.Text == "" || TBTenantsName1.Text == tBTenantsDefaultValue)
            {
                TBTenantsName1.Clear();
                TBTenantsName1.ForeColor = Color.Black;
            }
        }
        private void TBTenants1_Leave(object sender, EventArgs e)
        {
            if (TBTenantsName1.Text == "")
            {
                TBTenantsName1.ForeColor = Color.Gray;
                TBTenantsName1.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants2_Enter(object sender, EventArgs e)
        {
            if (TBTenantsName2.Text == "" || TBTenantsName2.Text == tBTenantsDefaultValue)
            {
                TBTenantsName2.Clear();
                TBTenantsName2.ForeColor = Color.Black;
            }
        }
        private void TBTenants2_Leave(object sender, EventArgs e)
        {
            if (TBTenantsName2.Text == "")
            {
                TBTenantsName2.ForeColor = Color.Gray;
                TBTenantsName2.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants3_Enter(object sender, EventArgs e)
        {
            if (TBTenantsName3.Text == "" || TBTenantsName3.Text == tBTenantsDefaultValue)
            {
                TBTenantsName3.Clear();
                TBTenantsName3.ForeColor = Color.Black;
            }
        }
        private void TBTenants3_Leave(object sender, EventArgs e)
        {
            if (TBTenantsName3.Text == "")
            {
                TBTenantsName3.ForeColor = Color.Gray;
                TBTenantsName3.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants4_Enter(object sender, EventArgs e)
        {
            if (TBTenantsName4.Text == "" || TBTenantsName4.Text == tBTenantsDefaultValue)
            {
                TBTenantsName4.Clear();
                TBTenantsName4.ForeColor = Color.Black;
            }
        }
        private void TBTenants4_Leave(object sender, EventArgs e)
        {
            if (TBTenantsName4.Text == "")
            {
                TBTenantsName4.ForeColor = Color.Gray;
                TBTenantsName4.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants5_Enter(object sender, EventArgs e)
        {
            if (TBTenantsName5.Text == "" || TBTenantsName5.Text == tBTenantsDefaultValue)
            {
                TBTenantsName5.Clear();
                TBTenantsName5.ForeColor = Color.Black;
            }
        }
        private void TBTenants5_Leave(object sender, EventArgs e)
        {
            if (TBTenantsName5.Text == "")
            {
                TBTenantsName5.ForeColor = Color.Gray;
                TBTenantsName5.Text = tBTenantsDefaultValue;
            }
        }

        private void TBTenants6_Enter(object sender, EventArgs e)
        {
            if (TBTenantsName6.Text == "" || TBTenantsName6.Text == tBTenantsDefaultValue)
            {
                TBTenantsName6.Clear();
                TBTenantsName6.ForeColor = Color.Black;
            }
        }
        private void TBTenants6_Leave(object sender, EventArgs e)
        {
            if (TBTenantsName6.Text == "")
            {
                TBTenantsName6.ForeColor = Color.Gray;
                TBTenantsName6.Text = tBTenantsDefaultValue;
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

        private void TBPhoneNumber_Leave(object sender, EventArgs e)
        {
                TBTenantsPhoneNumber1.Text = TBPhoneNumber.Text;
        }

        //Picture ID
        private void BPictureID_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Image";
            ofd.Filter = "Images|*.bmp; *.png; *.jpg; *.gif";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PBPictureID.Image = Image.FromFile(ofd.FileName);
                BPictureID.Hide();   
            }
        }

        //Save
        private void BSave_Click(object sender, EventArgs e)
        {
            // Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlSkeleton);

            //Populate
            //Main Tenant
            doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/First_Name").InnerText = TBFirstName.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/Middle_Initial").InnerText = TBMiddleInitial.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/Last_Name").InnerText = TBLastName.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/Phone_Number").InnerText = TBPhoneNumber.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/Ocupation").InnerText = TBOcupation.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/DOB").InnerText = DTPDOB.Value.ToString();

            doc.SelectSingleNode("/Apartment/Date/Tenants/Main_Tenant/ID_Location").InnerText = pictureIDLocation;

            //Tenant 2
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_2/Name").InnerText = TBTenantsName2.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_2/Phone_Number").InnerText = TBTenantsPhoneNumber2.Text;
            //Tenant 3
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_3/Name").InnerText = TBTenantsName3.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_3/Phone_Number").InnerText = TBTenantsPhoneNumber3.Text;
            //Tenant 4
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_4/Name").InnerText = TBTenantsName4.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_4/Phone_Number").InnerText = TBTenantsPhoneNumber4.Text;
            //Tenant 5
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_5/Name").InnerText = TBTenantsName5.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_5/Phone_Number").InnerText = TBTenantsPhoneNumber5.Text;
            //Tenant 6
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_6/Name").InnerText = TBTenantsName6.Text;
            doc.SelectSingleNode("/Apartment/Date/Tenants/Other_Tenants/Tenant_6/Phone_Number").InnerText = TBTenantsPhoneNumber6.Text;

            doc.SelectSingleNode("/Apartment/Date/Tenants/Starting_Date").InnerText = DTPStartingDay.Value.ToString();
            doc.SelectSingleNode("/Apartment/Date/Tenants/Ending_Date").InnerText = DTPEndingDay.Value.ToString();

            doc.SelectSingleNode("/Apartment/Date/Tenants/Tenants_Notes").InnerText = RTBTenantsNotes.Text;

            //Payments
            //Deposit
            doc.SelectSingleNode("/Apartment/Date/Payments/Deposit_Payment_Amount").InnerText = TBDepositAmount.Text;
            try
            {
                doc.SelectSingleNode("/Apartment/Date/Payments/Deposit_Payment_Type").InnerText = CBDepositPaymentType.SelectedItem.ToString();
            }
            catch (Exception) { }
            doc.SelectSingleNode("/Apartment/Date/Payments/Deposit_Payment_Date").InnerText = DTPRentPaymentDate.Value.ToString();

            //Rent
            doc.SelectSingleNode("/Apartment/Date/Payments/Rent_Payment_Amount").InnerText = TBRentAmount.Text;
            try
            {
                doc.SelectSingleNode("/Apartment/Date/Payments/Rent_Payment_Type").InnerText = CBRentPaymentType.SelectedItem.ToString();
            }
            catch (Exception) { }
            doc.SelectSingleNode("/Apartment/Date/Payments/Rent_Paymnet_Date").InnerText = TBRentPaymentDay.Text;

            //Notes
            doc.SelectSingleNode("/Apartment/Date/Payments/Payment_Notes").InnerText = RTBPaymentNotes.Text;

            //Utilitties
            //Gas
            doc.SelectSingleNode("/Apartment/Date/Utilities/Gas_We_Pay").InnerText = CBGas.Checked.ToString();
            doc.SelectSingleNode("/Apartment/Date/Utilities/Gas_Payment").InnerText = TBGas.Text;
            //Electric
            doc.SelectSingleNode("/Apartment/Date/Utilities/Electric_We_Pay").InnerText = CBElectric.Checked.ToString();
            doc.SelectSingleNode("/Apartment/Date/Utilities/Electric_Payment").InnerText = TBElectric.Text;
            //Watter
            doc.SelectSingleNode("/Apartment/Date/Utilities/Water_We_Pay").InnerText = CBWatter.Checked.ToString();
            doc.SelectSingleNode("/Apartment/Date/Utilities/Water_Payment").InnerText = TBWatter.Text;
            //Trash
            doc.SelectSingleNode("/Apartment/Date/Utilities/Trash_We_Pay").InnerText = CBTrash.Checked.ToString();
            doc.SelectSingleNode("/Apartment/Date/Utilities/Trash_Payment").InnerText = TBTrash.Text;
            //Notes
            doc.SelectSingleNode("/Apartment/Date/Utilities/Utilities_Notes").InnerText = RTBUtilitiesNotes.Text;

            //Monthly Notes
            doc.SelectSingleNode("/Apartment/Date/Notes/Other_Monthly_Expencess").InnerText = RTBOtherMonthlyExpencess.Text;
            doc.SelectSingleNode("/Apartment/Date/Notes/Additional_Notes").InnerText = RTBAdditionalNotes.Text;

            // Add a price element.
            XmlElement newElem = doc.CreateElement("price");
            newElem.InnerText = "10.95";
            doc.DocumentElement.AppendChild(newElem);

            // Save the document to a file and auto-indent the output.
            XmlTextWriter writer = new XmlTextWriter(xmlSaveFileLocation, null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }
    }
}
