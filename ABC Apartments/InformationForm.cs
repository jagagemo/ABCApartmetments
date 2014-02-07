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
        private string address;
        private string tBFirstNameDefaultValue = "Primer Nombre";
        private string tBMiddleInitialDefaultValue = "In";
        private string tBLastNameDefaultValue = "Apellido";
        private string tBTenantsDefaultValue = "Nombre Completo";
        private string pictureIDLocation = "";
        private string xmlSaveFileLocation;
 
        //Constructor
        public InformationForm(string title, Image image, string xmlSaveFileLocation){
            InitializeComponent();
            this.xmlSaveFileLocation = xmlSaveFileLocation;
            address = title;
            this.Text = address;
            PBApartmentImage.Image = image;

            populate(DateTime.Today);
            testDefaults();
        }

        private void populate(DateTime Date_InstanceValue)
        {
            //Set Defualt Calender Values.
            Calender.MaxDate = DateTime.Today;
            Calender.SetDate(Date_InstanceValue);
            Calender.TodayDate = Date_InstanceValue;
            Calender.SelectionEnd = DateTime.Today;

            //Test if the File Exists
            if (!System.IO.File.Exists(xmlSaveFileLocation))
            {
                return;
            }

            //Instanciate the XMLDocument object.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlSaveFileLocation);

            /*
            int i = 1;
            foreach (var ChildNodes in xmlDoc)
            {
                string start = xmlDoc.ChildNodes.Item(1).ChildNodes.Item(i).Attributes.GetNamedItem("Start_Day").Value;
                string end = xmlDoc.ChildNodes.Item(1).ChildNodes.Item(i).Attributes.GetNamedItem("End_Day").Value;

                if( (DateTime.Compare(Date_InstanceValue, DateTime.Parse(start)) >= 0 ) &&
                    (DateTime.Compare(Date_InstanceValue, DateTime.Parse(end)) <= 0) )
                {
                    break;
                }
                i++;
	        }

            Console.WriteLine(i);
            
            /*
            XmlNode temp;
            try
            {
                temp = xmlDoc.SelectSingleNode("//Address[@Start_Day='" + Date_InstanceValue + "']");
            }
            catch
            {

            }
            */

            //Start Day
            try
            {
                DTPStartingDay.Value = DateTime.Parse(xmlDoc.SelectSingleNode("/Apartment/Date_Instance").Attributes.GetNamedItem("Start_Day").Value);
            }
            catch { }

            //End Day
            try
            {
                DTPEndingDay.Value = DateTime.Parse(xmlDoc.SelectSingleNode("/Apartment/Date_Instance").Attributes.GetNamedItem("End_Day").Value);
            }
            catch { }

            //Calender
            try
            {
                Calender.SetDate(Date_InstanceValue);
            }
            catch{ }

            //Main Tenant
            TBFirstName.Text = xmlDoc.SelectSingleNode(    "/Apartment/Date_Instance/Tenants/Main_Tenant/First_Name").InnerText;
            TBMiddleInitial.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Tenants/Main_Tenant/Middle_Initial").InnerText;
            TBLastName.Text = xmlDoc.SelectSingleNode(     "/Apartment/Date_Instance/Tenants/Main_Tenant/Last_Name").InnerText;
            TBPhoneNumber.Text = xmlDoc.SelectSingleNode(  "/Apartment/Date_Instance/Tenants/Main_Tenant/Phone_Number").InnerText;
            TBOcupation.Text = xmlDoc.SelectSingleNode(    "/Apartment/Date_Instance/Tenants/Main_Tenant/Ocupation").InnerText;

            try
            {
                DTPDOB.Value = DateTime.Parse(xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Tenants/Main_Tenant/DOB").InnerText);
            }
            catch { }
            
            //Picture ID
            pictureIDLocation = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Tenants/Main_Tenant/ID_Location").InnerText;
            try
            {
                PBPictureID.Image = Image.FromFile(pictureIDLocation);
            }
            catch
            {
                pictureIDLocation = "";
            }

            //Other Tenants
            TBTenantsName1.Text = TBFirstName.Text + " " + TBMiddleInitial.Text + ". " + TBLastName.Text;
            TBTenantsPhoneNumber1.Text = TBPhoneNumber.Text;
            //Tenant 2
            TBTenantsName2.Text = xmlDoc.SelectSingleNode(         "/Apartment/Date_Instance/Tenants/Tenant_2/Name").InnerText;
            TBTenantsPhoneNumber2.Text = xmlDoc.SelectSingleNode(  "/Apartment/Date_Instance/Tenants/Tenant_2/Phone_Number").InnerText;
            //Tenant 3
            TBTenantsName3.Text = xmlDoc.SelectSingleNode(         "/Apartment/Date_Instance/Tenants/Tenant_3/Name").InnerText;
            TBTenantsPhoneNumber3.Text = xmlDoc.SelectSingleNode(  "/Apartment/Date_Instance/Tenants/Tenant_3/Phone_Number").InnerText;
            //Tenant 4
            TBTenantsName4.Text = xmlDoc.SelectSingleNode(         "/Apartment/Date_Instance/Tenants/Tenant_4/Name").InnerText;
            TBTenantsPhoneNumber4.Text = xmlDoc.SelectSingleNode(  "/Apartment/Date_Instance/Tenants/Tenant_4/Phone_Number").InnerText;
            //Tenant 5
            TBTenantsName5.Text = xmlDoc.SelectSingleNode(         "/Apartment/Date_Instance/Tenants/Tenant_5/Name").InnerText;
            TBTenantsPhoneNumber5.Text = xmlDoc.SelectSingleNode(  "/Apartment/Date_Instance/Tenants/Tenant_5/Phone_Number").InnerText;
            //Tenant 6
            TBTenantsName6.Text = xmlDoc.SelectSingleNode(         "/Apartment/Date_Instance/Tenants/Tenant_6/Name").InnerText;
            TBTenantsPhoneNumber6.Text = xmlDoc.SelectSingleNode(  "/Apartment/Date_Instance/Tenants/Tenant_6/Phone_Number").InnerText;

            RTBTenantsNotes.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Tenants/Tenants_Notes").InnerText;

            //Payments
            //Deposit
            TBDepositAmount.Text = xmlDoc.SelectSingleNode(                "/Apartment/Date_Instance/Payments/Deposit_Payment_Amount").InnerText;
            CBDepositPaymentType.SelectedItem = xmlDoc.SelectSingleNode(   "/Apartment/Date_Instance/Payments/Deposit_Payment_Type").InnerText;
            try
            {
                DTPRentPaymentDate.Value = DateTime.Parse(xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Payments/Deposit_Payment_Date").InnerText);
            }
            catch { }

            //Rent
            TBRentAmount.Text = xmlDoc.SelectSingleNode(               "/Apartment/Date_Instance/Payments/Rent_Payment_Amount").InnerText;
            CBRentPaymentType.SelectedItem = xmlDoc.SelectSingleNode(  "/Apartment/Date_Instance/Payments/Rent_Payment_Type").InnerText;
            TBRentPaymentDay.Text = xmlDoc.SelectSingleNode(           "/Apartment/Date_Instance/Payments/Rent_Paymnet_Date").InnerText;

            //Notes
            RTBPaymentNotes.Text = xmlDoc.SelectSingleNode(            "/Apartment/Date_Instance/Payments/Payment_Notes").InnerText;

            //Utilitties
            //Gas
            try
            {
                CBGas.Checked = Boolean.Parse(xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Utilities/Gas_We_Pay").InnerText);
            }
            catch(Exception){
                CBGas.Checked = false;
            }
            TBGas.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Utilities/Gas_Payment").InnerText;
            //Electric
            try
            {
                CBElectric.Checked = Boolean.Parse(xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Utilities/Electric_We_Pay").InnerText);
            }
            catch (Exception)
            {
                CBElectric.Checked = false;
            }
            TBElectric.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Utilities/Electric_Payment").InnerText;
            //Watter
            try
            {
                CBWatter.Checked = Boolean.Parse(xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Utilities/Water_We_Pay").InnerText);
            }
            catch (Exception)
            {
                CBWatter.Checked = false;
            }
            TBWatter.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Utilities/Water_Payment").InnerText;
            //Trash
            try
            {
                CBTrash.Checked = Boolean.Parse(xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Utilities/Trash_We_Pay").InnerText);
            }
            catch (Exception)
            {
                CBTrash.Checked = false;
            }
            TBTrash.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Utilities/Trash_Payment").InnerText;
            //Notes
            RTBUtilitiesNotes.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Utilities/Utilities_Notes").InnerText;

            //Monthly Notes
            RTBOtherMonthlyExpencess.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Notes/Other_Monthly_Expencess").InnerText;
            RTBAdditionalNotes.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Notes/Additional_Notes").InnerText;
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
                pictureIDLocation = ofd.FileName;
                PBPictureID.Image = Image.FromFile(pictureIDLocation);
                BPictureID.Hide(); 
            }
        }

        //Save
        private void BSave_Click(object sender, EventArgs e)
        {
            // Create the XmlDocument.
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmlDoc.AppendChild(xmlNode);

            XmlElement xmlElem;

            ////Apartment////
            xmlDoc.AppendChild(xmlDoc.CreateElement("", "Apartment", ""));

            //Adress
            xmlElem = xmlDoc.CreateElement("", "Address", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(address));
            xmlDoc.ChildNodes.Item(1).AppendChild(xmlElem);

            ////Date Instance////
            xmlElem = xmlDoc.CreateElement("", "Date_Instance", "");
            xmlElem.SetAttribute("Start_Day", DTPStartingDay.Value.ToString());
            xmlElem.SetAttribute("End_Day", DTPEndingDay.Value.ToString());
            //xmlDoc.ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Date_Instance", ""));
            xmlDoc.ChildNodes.Item(1).AppendChild(xmlElem);
            
            //Date
            xmlElem = xmlDoc.CreateElement("", "Last_Modified_Date", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(Calender.SelectionStart.Date.ToString()));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlElem);
            
            ////Tenants////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenants", ""));

            ////Main Tenant////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Main_Tenant", ""));


            //First Name
            xmlElem = xmlDoc.CreateElement("", "First_Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBFirstName.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).AppendChild(xmlElem);

            //Middle Initial
            xmlElem = xmlDoc.CreateElement("", "Middle_Initial", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBMiddleInitial.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).AppendChild(xmlElem);

            //Last Name
            xmlElem = xmlDoc.CreateElement("", "Last_Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBLastName.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBPhoneNumber.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).AppendChild(xmlElem);

            //Ocupation
            xmlElem = xmlDoc.CreateElement("", "Ocupation", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBOcupation.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).AppendChild(xmlElem);

            //DOB
            xmlElem = xmlDoc.CreateElement("", "DOB", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(DTPDOB.Value.ToString()));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).AppendChild(xmlElem);

            //ID_Location
            xmlElem = xmlDoc.CreateElement("", "ID_Location", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(pictureIDLocation));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).AppendChild(xmlElem);
            
            /**End of Main Tenant**/

            ////Tenant 2////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_2", ""));

            //Name
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsName2.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber2.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlElem);

            /**End of Tenant 2**/

            ////Tenant 3////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_3", ""));

            //Name
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsName3.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber3.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            /**End of Tenant 3**/

            ////Tenant 4////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_4", ""));

            //Name
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsName4.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber4.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            /**End of Tenant 4**/

            ////Tenant 5////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_5", ""));

            //Name
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsName5.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(4).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber5.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(4).AppendChild(xmlElem);

            /**End of Tenant 5**/

            ////Tenant 6////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_6", ""));

            //Name
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsName6.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(5).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber6.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(5).AppendChild(xmlElem);

            /**End of Tenant 6**/

/*            //Starting Date
            xmlElem = xmlDoc.CreateElement("", "Starting_Date", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(DTPStartingDay.Value.ToString()));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlElem);

            //Ending Date
            xmlElem = xmlDoc.CreateElement("", "Ending_Date", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(DTPEndingDay.Value.ToString()));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlElem);
*/
            //Tenants Notes
            xmlElem = xmlDoc.CreateElement("", "Tenants_Notes", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(RTBTenantsNotes.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlElem);

            /**End of Tenants**/

            ////Payments////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Payments", ""));

            //Deposit Payment Amount
            xmlElem = xmlDoc.CreateElement("", "Deposit_Payment_Amount", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBDepositAmount.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            //Deposit Payment Type
            xmlElem = xmlDoc.CreateElement("", "Deposit_Payment_Type", "");
            try{
                xmlElem.AppendChild(xmlDoc.CreateTextNode(CBDepositPaymentType.SelectedItem.ToString()));
            }catch (Exception){
                xmlElem.AppendChild(xmlDoc.CreateTextNode(""));
            }
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            //Deposit Payment Date
            xmlElem = xmlDoc.CreateElement("", "Deposit_Payment_Date", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(DTPRentPaymentDate.Value.ToString()));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);
            
            //Rent Payment Amount
            xmlElem = xmlDoc.CreateElement("", "Rent_Payment_Amount", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBRentAmount.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            //Rent Payment Type
            xmlElem = xmlDoc.CreateElement("", "Rent_Payment_Type", "");
            try
            {
                xmlElem.AppendChild(xmlDoc.CreateTextNode(CBRentPaymentType.SelectedItem.ToString()));
            }
            catch (Exception)
            {
                xmlElem.AppendChild(xmlDoc.CreateTextNode(""));
            }
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            //Rent Payment Date
            xmlElem = xmlDoc.CreateElement("", "Rent_Paymnet_Date", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBRentPaymentDay.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            //Payment Notes
            xmlElem = xmlDoc.CreateElement("", "Payment_Notes", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(RTBPaymentNotes.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            /**End of Payments**/

            ////Utilities////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Utilities", ""));

            //Gas We Pay
            xmlElem = xmlDoc.CreateElement("", "Gas_We_Pay", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(CBGas.Checked.ToString()));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Gas Payment
            xmlElem = xmlDoc.CreateElement("", "Gas_Payment", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBGas.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Electric We Pay
            xmlElem = xmlDoc.CreateElement("", "Electric_We_Pay", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(CBElectric.Checked.ToString()));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Electric Payment
            xmlElem = xmlDoc.CreateElement("", "Electric_Payment", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBElectric.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Water We Pay
            xmlElem = xmlDoc.CreateElement("", "Water_We_Pay", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(CBWatter.Checked.ToString()));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Water Payment
            xmlElem = xmlDoc.CreateElement("", "Water_Payment", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBWatter.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Trash We Pay
            xmlElem = xmlDoc.CreateElement("", "Trash_We_Pay", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(CBTrash.Checked.ToString()));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Trash Payment
            xmlElem = xmlDoc.CreateElement("", "Trash_Payment", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTrash.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Utilities Notes
            xmlElem = xmlDoc.CreateElement("", "Utilities_Notes", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(RTBUtilitiesNotes.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            /***End of Utilities***/

            ////Notes////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Notes", ""));

            //Other Monthly Expencess
            xmlElem = xmlDoc.CreateElement("", "Other_Monthly_Expencess", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(RTBOtherMonthlyExpencess.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(4).AppendChild(xmlElem);

            //Additional Notes
            xmlElem = xmlDoc.CreateElement("", "Additional_Notes", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(RTBAdditionalNotes.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(4).AppendChild(xmlElem);

            /**/
            try{
                xmlDoc.Save(xmlSaveFileLocation);
			}catch (Exception er){
				Console.WriteLine(er.Message);
			}
        }
    }
}