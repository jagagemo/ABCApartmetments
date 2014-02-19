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
        /// <summary>
        /// Global Varables
        /// </summary>
        /// The string value of the apartment address.
        private string address;

        ///Hold the '/Address/Date_Instance [@Start="foo" and @End="bar"]/' string used to find the instance origin.
        private string basePath;

        ///String varables used at runtime to populate as default values in lieu of a valid entry.
        private string tBFirstNameDefaultValue = "Primer Nombre";
        private string tBMiddleInitialDefaultValue = "In";
        private string tBLastNameDefaultValue = "Apellido";
        private string tBTenantsDefaultValue = "Nombre Completo";

        ///String holding the file location of the ID picture.
        private string pictureIDLocation = "";
        
        ///Working file.
        private string xmlSaveFileLocation;
        Boolean entryExists;

        ///Working global varables.
        
        ///Holds all date covered by the currently selected file entry.
        private DateTime[] rangeOfDates;
        //*********************************************************************************************************************************************//
        ///Constructor
        //*********************************************************************************************************************************************//
        public InformationForm(string title, Image image, string xmlSaveFileLocation){
            InitializeComponent();
            this.xmlSaveFileLocation = xmlSaveFileLocation;
            address = title;
            this.Text = address;
            PBApartmentImage.Image = image;

            populateForm(DateTime.Today.Date);
        }

        //*********************************************************************************************************************************************//
        //Load function.
        //*********************************************************************************************************************************************//
        private void populateForm(DateTime Date_InstanceValue)
        {
            //Set defautl calender values
            setAllCalenderlimits(Date_InstanceValue);

            //Test if the File Exists
            if (!System.IO.File.Exists(xmlSaveFileLocation))
            {
                return;
            }

            //Instanciate the XMLDocument object.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlSaveFileLocation);

           // XML_Date_Instance_Helper helper = new XML_Date_Instance_Helper(xmlDoc, Date_InstanceValue);

            entryExists = false;
            int i = 0;

            DateTime start = DateTime.Today.Date;
            DateTime end = DateTime.Today.Date;
            foreach (XmlNode node in xmlDoc.ChildNodes.Item(1).ChildNodes)
            {
                if (i != 0)
                {
                    start = DateTime.Parse(node.Attributes.GetNamedItem("Start_Day").Value);
                    end = DateTime.Parse(node.Attributes.GetNamedItem("End_Day").Value);
                    
                    if (Date_InstanceValue >= start.Date && Date_InstanceValue <= end.Date)
                    {
                        //Console.WriteLine("hurp");
                        entryExists = true;
                        rangeOfDates = populateRangeOfDates(start, end);
                        Calender.BoldedDates = rangeOfDates;
                        break;
                    }                    
                }
                i++;
	        }

            if (!entryExists)
            {
                populateFormWithDefaultValues(Date_InstanceValue);
                return;
            }

            //This Varable driects to the base path for the working data instance.
            basePath = "/Apartment/Date_Instance[@Start_Day='" + start.Date.ToString() + "' and @End_Day='" + end.Date.ToString() + "']";

            //Start Day
            DTPStartingDay.Value = DateTime.Parse(start.ToString());

            //End Day
            DTPEndingDay.Value = DateTime.Parse(end.ToString());

            //Calender
            Calender.SetDate(Date_InstanceValue);
            return;
            //Main Tenant
            TBFirstName.Text = xmlDoc.SelectSingleNode(     basePath + "/Tenants/Main_Tenant/First_Name").InnerText;
            TBMiddleInitial.Text = xmlDoc.SelectSingleNode( basePath + "/Tenants/Main_Tenant/Middle_Initial").InnerText;
            TBLastName.Text = xmlDoc.SelectSingleNode(      basePath + "/Tenants/Main_Tenant/Last_Name").InnerText;
            TBPhoneNumber.Text = xmlDoc.SelectSingleNode(   basePath + "/Tenants/Main_Tenant/Phone_Number").InnerText;
            TBOcupation.Text = xmlDoc.SelectSingleNode(     basePath + "/Tenants/Main_Tenant/Ocupation").InnerText;

            //-Date of Birth.
            try
            {
                DTPDOB.Value = DateTime.Parse(xmlDoc.SelectSingleNode(basePath + "/Tenants/Main_Tenant/DOB").InnerText);
            }
            catch
            {
                DialogResult dr = MessageBox.Show(
                    "La Fecha De Nacimiento es invalida." +
                    "\n\n*Encaso de un menor de edad favor de poner la informacion del guardian como la \"Persona Responsable\"." + 
                    "\n\n*Si este mensaje le parece erronio, es posible que el archivo se haya corompido. Favor de contactar al Encargado de IT.", 
                        "Error en la Fecha De Nacimiento", MessageBoxButtons.OK);
            }

            //Picture ID
            pictureIDLocation = xmlDoc.SelectSingleNode(basePath + "/Tenants/Main_Tenant/ID_Location").InnerText;
            try
            {
                PBPictureID.Image = Image.FromFile(pictureIDLocation);
                if (PBPictureID.Image != null)
                {
                    BPictureID.Visible = false;
                }
            }
            catch
            {
                Console.WriteLine("No valid picture ID file.");
                pictureIDLocation = "";
            }

            //Other Tenants
            TBTenantsName1.Text = TBFirstName.Text + " " + TBMiddleInitial.Text + ". " + TBLastName.Text;
            TBTenantsPhoneNumber1.Text = TBPhoneNumber.Text;
            //Tenant 2
            TBTenantsName2.Text = xmlDoc.SelectSingleNode(          basePath + "/Tenants/Tenant_2/Name").InnerText;
            TBTenantsPhoneNumber2.Text = xmlDoc.SelectSingleNode(   basePath + "/Tenants/Tenant_2/Phone_Number").InnerText;
            //Tenant 3
            TBTenantsName3.Text = xmlDoc.SelectSingleNode(          basePath + "/Tenants/Tenant_3/Name").InnerText;
            TBTenantsPhoneNumber3.Text = xmlDoc.SelectSingleNode(   basePath + "/Tenants/Tenant_3/Phone_Number").InnerText;
            //Tenant 4
            TBTenantsName4.Text = xmlDoc.SelectSingleNode(          basePath + "/Tenants/Tenant_4/Name").InnerText;
            TBTenantsPhoneNumber4.Text = xmlDoc.SelectSingleNode(   basePath + "/Tenants/Tenant_4/Phone_Number").InnerText;
            //Tenant 5
            TBTenantsName5.Text = xmlDoc.SelectSingleNode(          basePath + "/Tenants/Tenant_5/Name").InnerText;
            TBTenantsPhoneNumber5.Text = xmlDoc.SelectSingleNode(   basePath + "/Tenants/Tenant_5/Phone_Number").InnerText;
            //Tenant 6
            TBTenantsName6.Text = xmlDoc.SelectSingleNode(          basePath + "/Tenants/Tenant_6/Name").InnerText;
            TBTenantsPhoneNumber6.Text = xmlDoc.SelectSingleNode(   basePath + "/Tenants/Tenant_6/Phone_Number").InnerText;

            RTBTenantsNotes.Text = xmlDoc.SelectSingleNode(         basePath + "/Tenants/Tenants_Notes").InnerText;
            

            //Payments
            //Deposit
            TBDepositAmount.Text = xmlDoc.SelectSingleNode(                 basePath + "/Payments/Deposit_Payment_Amount").InnerText;
            CBDepositPaymentType.SelectedItem = xmlDoc.SelectSingleNode(    basePath + "/Payments/Deposit_Payment_Type").InnerText;
            try
            {
                DTPRentPaymentDate.Value = DateTime.Parse(xmlDoc.SelectSingleNode(basePath + "/Payments/Deposit_Payment_Date").InnerText);
            }
            catch
            {
                DialogResult dr = MessageBox.Show(
                    "La Fecha del Depocito es invalida." +
                    "\n\n*Si este mensaje le parece erronio, es posible que el archivo se haya corompido. Favor de contactar al Encargado de IT.",
                        "Error en la La Fecha del Depocito", MessageBoxButtons.OK);
            }

            //Rent
            TBRentAmount.Text = xmlDoc.SelectSingleNode(               basePath + "/Payments/Rent_Payment_Amount").InnerText;
            CBRentPaymentType.SelectedItem = xmlDoc.SelectSingleNode(  basePath + "/Payments/Rent_Payment_Type").InnerText;
            TBRentPaymentDay.Text = xmlDoc.SelectSingleNode(           basePath + "/Payments/Rent_Paymnet_Date").InnerText;

            //Notes
            RTBPaymentNotes.Text = xmlDoc.SelectSingleNode(            basePath + "/Payments/Payment_Notes").InnerText;

            ///Utilitties
            //Gas
            try
            {
                CBGas.Checked = Boolean.Parse(xmlDoc.SelectSingleNode(basePath + "/Utilities/Gas_We_Pay").InnerText);
            }
            catch(Exception e){
                Console.WriteLine(e);
                CBGas.Checked = false;
            }
            TBGas.Text = xmlDoc.SelectSingleNode(basePath + "/Utilities/Gas_Payment").InnerText;

            //Electric
            try
            {
                CBElectric.Checked = Boolean.Parse(xmlDoc.SelectSingleNode(basePath + "/Utilities/Electric_We_Pay").InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CBElectric.Checked = false;
            }
            TBElectric.Text = xmlDoc.SelectSingleNode(basePath + "/Utilities/Electric_Payment").InnerText;

            //Watter
            try
            {
                CBWatter.Checked = Boolean.Parse(xmlDoc.SelectSingleNode(basePath + "/Utilities/Water_We_Pay").InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CBWatter.Checked = false;
            }
            TBWatter.Text = xmlDoc.SelectSingleNode(basePath + "/Utilities/Water_Payment").InnerText;

            //Trash
            try
            {
                CBTrash.Checked = Boolean.Parse(xmlDoc.SelectSingleNode(basePath + "/Utilities/Trash_We_Pay").InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CBTrash.Checked = false;
            }
            TBTrash.Text = xmlDoc.SelectSingleNode(basePath + "/Utilities/Trash_Payment").InnerText;

            //Notes
            RTBUtilitiesNotes.Text = xmlDoc.SelectSingleNode(basePath + "/Utilities/Utilities_Notes").InnerText;

            //Monthly Notes
            RTBOtherMonthlyExpencess.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Notes/Other_Monthly_Expencess").InnerText;
            RTBAdditionalNotes.Text = xmlDoc.SelectSingleNode("/Apartment/Date_Instance/Notes/Additional_Notes").InnerText;
        }



        //*********************************************************************************************************************************************//
        //Load default values function. This function is called if the file does not exist.
        //*********************************************************************************************************************************************//
        private void populateFormWithDefaultValues(DateTime Date_InstanceValue)
        {
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

        //*********************************************************************************************************************************************//
        //Populate the file with the skeleton
        //*********************************************************************************************************************************************//
        private void addSkeletonToFile(string start, string end)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement xmlElem;

            ///Date Instance
            xmlElem = xmlDoc.CreateElement("", "Date_Instance", "");
            xmlElem.SetAttribute("Start_Day", start);
            xmlElem.SetAttribute("End_Day", end);
            xmlDoc.SelectSingleNode("/Apartment").AppendChild(xmlElem);

            //Date
            xmlElem = xmlDoc.CreateElement("", "Last_Modified_Date", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(""));
            xmlDoc.SelectSingleNode(basePath).AppendChild(xmlElem);
        }
        //*********************************************************************************************************************************************//
        //Save function
        //*********************************************************************************************************************************************//
        private void BSave_Click(object sender, EventArgs e)
        {
            // Create the XmlDocument.
            XmlDocument xmlDoc = new XmlDocument();

            //Creat xmlElement for value testing and passing.
            XmlElement xmlElem;

            string start = DTPStartingDay.Value.Date.ToString();
            string end = DTPEndingDay.Value.Date.ToString();

            basePath = "/Apartment/Date_Instance[@Start_Day='" + start + "' and @End_Day='" + end + "']";

            //Test if the File Exists
            if (!System.IO.File.Exists(xmlSaveFileLocation))
            {
                XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xmlDoc.AppendChild(xmlNode);

                ////Apartment////
                xmlDoc.AppendChild(xmlDoc.CreateElement("", "Apartment", ""));

                //Adress
                xmlElem = xmlDoc.CreateElement("", "Address", "");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(address));
                xmlDoc.SelectSingleNode("/Apartment").AppendChild(xmlElem);

                //Create Entry
                addSkeletonToFile(start, end);
                entryExists = true;
            }
            else
            {
                xmlDoc.Load(xmlSaveFileLocation);
            }

            ///Test if the entry exsits if it dose not make add it.
            if (!entryExists)
            {
                addSkeletonToFile(start, end);
                return;
            }
            
            //Date
            //xmlElem = xmlDoc.CreateElement("", "Last_Modified_Date", "");
            //xmlElem.AppendChild(xmlDoc.CreateTextNode(Calender.SelectionStart.Date.ToString()));
            xmlDoc.SelectSingleNode(basePath).Value = Calender.SelectionStart.Date.ToString();// AppendChild(xmlElem);
/*            
            ////Tenants////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenants", ""));

            ////Main Tenant////
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Main_Tenant", ""));


            //First Name
            string temp = "";
            if (TBFirstName.Text != tBFirstNameDefaultValue)
            {
                temp = TBFirstName.Text;
            }
            xmlElem = xmlDoc.CreateElement("", "First_Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(temp));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).AppendChild(xmlElem);

            //Middle Initial
            temp = "";
            if (TBMiddleInitial.Text != tBMiddleInitialDefaultValue)
            {
                temp = TBMiddleInitial.Text;
            }
            xmlElem = xmlDoc.CreateElement("", "Middle_Initial", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(temp));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(0).AppendChild(xmlElem);

            //Last Name
            temp = "";
            if (TBLastName.Text != tBLastNameDefaultValue)
            {
                temp = TBLastName.Text;
            }
            xmlElem = xmlDoc.CreateElement("", "Last_Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(temp));
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
            
            ///End of Main Tenant

            ///Tenant 2
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_2", ""));

            //Name
            temp = "";
            if (TBTenantsName2.Text != tBTenantsDefaultValue)
            {
                temp = TBTenantsName2.Text;
            }
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(temp));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlElem);
            
            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber2.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlElem);

            ///End of Tenant 2

            ///Tenant 3
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_3", ""));

            //Name
            temp = "";
            if (TBTenantsName3.Text != tBTenantsDefaultValue)
            {
                temp = TBTenantsName3.Text;
            }
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(temp));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber3.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(2).AppendChild(xmlElem);

            ///End of Tenant 3

            ///Tenant 4
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_4", ""));

            //Name
            temp = "";
            if (TBTenantsName4.Text != tBTenantsDefaultValue)
            {
                temp = TBTenantsName4.Text;
            }
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(temp));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber4.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(3).AppendChild(xmlElem);

            ///End of Tenant 4

            ///Tenant 5
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_5", ""));

            //Name
            temp = "";
            if (TBTenantsName5.Text != tBTenantsDefaultValue)
            {
                temp = TBTenantsName5.Text;
            }
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(temp));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(4).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber5.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(4).AppendChild(xmlElem);

            ///End of Tenant 5

            ///Tenant 6
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlDoc.CreateElement("", "Tenant_6", ""));

            //Name
            temp = "";
            if (TBTenantsName6.Text != tBTenantsDefaultValue)
            {
                temp = TBTenantsName6.Text;
            }
            xmlElem = xmlDoc.CreateElement("", "Name", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(temp));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(5).AppendChild(xmlElem);

            //Phone Number
            xmlElem = xmlDoc.CreateElement("", "Phone_Number", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(TBTenantsPhoneNumber6.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(5).AppendChild(xmlElem);

            ///End of Tenant 6

            //Tenants Notes
            xmlElem = xmlDoc.CreateElement("", "Tenants_Notes", "");
            xmlElem.AppendChild(xmlDoc.CreateTextNode(RTBTenantsNotes.Text));
            xmlDoc.ChildNodes.Item(1).ChildNodes.Item(1).ChildNodes.Item(1).AppendChild(xmlElem);

            ///End of Tenants

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

            ///End of Payments

            ///Utilities
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

            ///End of Utilities

            ///Notes
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
                entryExists = true;
			}catch (Exception er){
				Console.WriteLine(er.Message);
			}
        }

        //*********************************************************************************************************************************************//
        //Helper functions
        //*********************************************************************************************************************************************//
        private void setAllCalenderlimits(DateTime Date_InstanceValue)
        {
            //Set Defualt Calender Values.
            Calender.MaxDate = DateTime.Today;
            Calender.SetDate(Date_InstanceValue);
            Calender.TodayDate = Date_InstanceValue;
            Calender.RemoveAllBoldedDates();

            //Set Limit for date of Birth.
            DTPDOB.MaxDate = DateTime.Now.AddYears(-18);

        }

        public static DateTime[] populateRangeOfDates(DateTime startDate, DateTime endDate)
        {
            var total = new List<DateTime>();
            while (startDate <= endDate)
            {
                total.Add(startDate);
                startDate = startDate.AddDays(1);
            }
            return total.ToArray();
        }

        //*********************************************************************************************************************************************//
        //GUI control.
        //*********************************************************************************************************************************************//

        private void Calender_DateChanged(object sender, DateRangeEventArgs e)
        {
            // test if the array is empty or only has one element.
            if(rangeOfDates.Length <= 1){
                return;
            }

            // Test if the newly selected date is still within the range of the currently selected instance.
            DateTime selectedDate = Calender.SelectionRange.Start.Date;
            Boolean range_GT_EQ_Start = (rangeOfDates[0].Date <= selectedDate);
            Boolean range_LT_EQ_End = (rangeOfDates[rangeOfDates.Length - 1].Date <= selectedDate);

            if (!range_GT_EQ_Start && !range_LT_EQ_End){   
                populateForm(Calender.SelectionRange.Start.Date);
            }
        }

        private void DTPStartingDay_ValueChanged(object sender, EventArgs e)
        {
            if (DTPStartingDay.Value > DTPEndingDay.Value)
            {
                DTPStartingDay.Value = DTPEndingDay.Value;

                //Give an alert to to the user that the end date can't happen before the start date.
                DialogResult dr = MessageBox.Show("la fecha inicial no puede ser despues de la fecha final.", "Fecha Invalida", MessageBoxButtons.OK);
            }
        }

        private void DTPEndingDay_ValueChanged(object sender, EventArgs e)
        {
            if (DTPStartingDay.Value > DTPEndingDay.Value)
            {
                DTPEndingDay.Value = DTPStartingDay.Value;

                //Give an alert to to the user that the end date can't happen before the start date.
                DialogResult dr = MessageBox.Show("la fecha final no puede ser antes de la fecha inicial.", "Fecha Invalida", MessageBoxButtons.OK);
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

        private void updateFristTenantsName()
        {
            string firstName = "";
            string middleInital = "";
            string lastName = "";

            if (TBFirstName.Text != tBFirstNameDefaultValue)
            {
                firstName = TBFirstName.Text;
            }
            if (TBMiddleInitial.Text != tBMiddleInitialDefaultValue)
            {
                middleInital = TBMiddleInitial.Text + ".";
            }
            if (TBLastName.Text != tBLastNameDefaultValue)
            {
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
            if (CBGas.Checked)
            {
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
    }
}