using CERTENROLLLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CertReqClient
{
    public partial class lblname : Form
    {
        public lblname()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Calling FillCountryDropDown() Method
            FillCountryDropDown();

            subjectType.Items.Add("DNS-Name");
            subjectType.Items.Add("IP-Address");

            // enable textbox by default
            textbox_alternativeNames.Enabled = false;
            // TabControl - Disable/Enable tab page
        
        }

        
        private void Btn_generate_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(textBox_commonName.Text) && (!String.IsNullOrWhiteSpace(textBox_commonName.Text)))
            {
                // Read all Input Fields
                CertificateRequest myRequest = ReadInputValues();

                if (GetSpecialCharacter(myRequest).Count <= 0)
                {
                    // Define Settings for SaveFileDialog
                    SaveFileDialog saveFileDialog = SaveDialogSettings(myRequest);
                    // Returns path for Console Class
                    CertificateHandler myCerHandler = new CertificateHandler();
                    string path = CreateCsrFile(myRequest, saveFileDialog, myCerHandler);

                    /////////////////////// CONSOLE ////////////////////////
                    CertreqConsole myConsole = new CertreqConsole();

                    // calling method for console commands
                    myConsole.SubmitCertificate(path);
                    myConsole.AcceptCertificate(path);
                }
                else {

                    string specialChar = "";
                    foreach (string item in GetSpecialCharacter(myRequest))
                    {
                        specialChar += item;
                    }
                    MessageBox.Show("The following Characters are not allowed: " + specialChar);
                }
            }
            else
            {
                MessageBox.Show("Please enter the Domain.");
            }
        }

        private CertificateRequest ReadInputValues()
        {
            CertificateRequest myRequest = new CertificateRequest();
            myRequest.CommonName = textBox_commonName.Text;
            myRequest.SubjectAlternativeName = textbox_alternativeNames.Text;            
            myRequest.Organization = textbox_organization.Text;
            myRequest.Department = textBox_Department.Text;
            myRequest.City = textBox_City.Text;
            myRequest.State = textBox_State.Text;
            myRequest.Country = comboBox_country.SelectedValue.ToString();
            return myRequest;
        }

        private SaveFileDialog SaveDialogSettings(CertificateRequest myRequest)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FileName = myRequest.CommonName + ".txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            return saveFileDialog1;
        }

        private string CreateCsrFile(CertificateRequest myRequest, SaveFileDialog saveFileDialog1, CertificateHandler myCerHandler)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Code to write the stream goes here.
                string filename = saveFileDialog1.FileName;
                var request = myCerHandler.GenerateSigningRequest(myRequest);

                File.WriteAllText(filename, request);

                // create full path for console commands
                string path = Path.GetDirectoryName(filename) + "/" + Path.GetFileNameWithoutExtension(filename);

                return path;
            }
            return null;
        }

        private void FillCountryDropDown()
        {
            // filling the combobox
            comboBox_country.DisplayMember = "Value";
            comboBox_country.ValueMember = "Key";

            IEnumerable<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures).OrderBy(culture => culture.EnglishName);
            
            Dictionary<string, string> countries = new Dictionary<string, string>();
            countries.Add("", "");

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.Name);
                if (countries.ContainsKey(region.TwoLetterISORegionName) == false)
                {
                    countries.Add(region.TwoLetterISORegionName, region.EnglishName);
                }
            }

            comboBox_country.DataSource = new BindingSource(countries, null);
            comboBox_country.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        
                
        private void addSanBtn_Click(object sender, EventArgs e)
        {
            //if (subjectTypeInput.Text != "" )
            if (!String.IsNullOrEmpty(subjectTypeInput.Text) && (!String.IsNullOrEmpty(subjectType.Text)))
            {
                string subjectTypeName = "";
                switch (subjectType.Text)
                {
                    case "DNS-Name":
                        subjectTypeName = "dns=";
                        break;
                    case "IP-Address":
                        subjectTypeName = "ipaddress=";
                        break;
                    default:
                        break;
                }
                textbox_alternativeNames.Text += subjectTypeName + subjectTypeInput.Text + "\r\n";
                // Clear fields for new input
                subjectTypeInput.Text = "";
                subjectType.Text = "";
            }
            else
            {
                MessageBox.Show("Subject Alernative Name or SAN Type is missing.");
            }
        }


        private List<string> GetSpecialCharacter(CertificateRequest myRequest)
        {
            string[] specialChars = new string[] { "+", ",", "\"", ";"};
            List<string> charsIn = new List<string>();
            string[] inputFields = new string[] {myRequest.Organization, myRequest.CommonName, myRequest.Department, myRequest.City, myRequest.State, myRequest.Country};

            foreach (var item in specialChars)
            {
                foreach (var fields in inputFields)
                {
                    if (fields.Contains(item))
                    {
                        if (!charsIn.Contains(item))
                        {
                            charsIn.Add(item);
                        }
                    }
                }
            }
            return charsIn;
        }


        private void editSAN_Click(object sender, EventArgs e)
        {
            if (textbox_alternativeNames.Enabled == false)
            {
                textbox_alternativeNames.Enabled = true;
                editSAN.Text = "close edit mode";
            }
            else if (textbox_alternativeNames.Enabled == true)
            {
                textbox_alternativeNames.Enabled = false;
                editSAN.Text = "edit SAN";
            }   
        }

        private void generateCsrBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox_commonName.Text) && (!String.IsNullOrWhiteSpace(textBox_commonName.Text)))
            {
                tabControl1.SelectTab(tabPage2);
                SetDataForOverview();
            }
            else
            {
                MessageBox.Show("Please enter the Domain.");
            }
        }

        private void SetDataForOverview() {

            CertificateRequest myRequest = ReadInputValues();

            tb_subAltNames.ScrollBars = ScrollBars.Both;
            tb_subAltNames.WordWrap = false;
            tb_subAltNames.ReadOnly = true;

            lbl_domain.Text = myRequest.CommonName;
            tb_subAltNames.Text = myRequest.SubjectAlternativeName;
            lbl_organization.Text = myRequest.Organization;
            lbl_department.Text = myRequest.Department;
            lbl_city.Text = myRequest.City;
            lbl_state.Text = myRequest.State;
            
            var splitString = comboBox_country.SelectedItem.ToString().Split(',');
            string firstSplit = splitString[1];

            var splitSpecialChar = firstSplit.Split(']');
            lbl_country.Text = splitSpecialChar[0];
        }

        private void overviewBackBtn_Click(object sender, EventArgs e)
        {
             tabControl1.SelectTab(tabPage1);
        }
    }
}

