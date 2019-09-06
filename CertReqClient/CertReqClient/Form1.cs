﻿using CERTENROLLLib;
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

        CertreqConsole myConsole = new CertreqConsole();
        CertificateRequest myRequest = new CertificateRequest();
        DialogResult dialogResult;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Calling FillCountryDropDown() Method
            FillCountryDropDown();

            subjectType.Items.Add(messages.dnsName);
            subjectType.Items.Add(messages.ipAddress);

            // enable textbox by default
            textbox_alternativeNames.Enabled = false;
        }

        
        private void Btn_generate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox_commonName.Text) && (!String.IsNullOrWhiteSpace(textBox_commonName.Text)))
            {
                // Read all Input Fields
                CertificateRequest myRequest = ReadInputValues();

                List<string> specialCharacters = GetSpecialCharacter(myRequest);
                if (specialCharacters.Count <= 0)
                {
                    string path = SaveCsrFile(myConsole, myRequest);
                    // calling method for console commands
                    myConsole.SubmitCertificate(path);
                    myConsole.AcceptCertificate(path);
                }
                else {
                    string specialChar = string.Join("", specialCharacters);
                    MessageBox.Show(messages.charactersNotAllowed + " " + specialChar);
                }
            }
            else
            {
                MessageBox.Show(messages.enterDomain);
            }
        }

        private CertificateRequest ReadInputValues()
        {
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
                        subjectTypeName = messages.dnsEquals;
                        break;
                    case "IP-Address":
                        subjectTypeName = messages.ipaddressEquals;
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
                MessageBox.Show(messages.missingSAN);
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
                editSAN.Text = messages.closeEditMode;
            }
            else if (textbox_alternativeNames.Enabled == true)
            {
                textbox_alternativeNames.Enabled = false;
                editSAN.Text = messages.editSan;
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
                MessageBox.Show(messages.enterDomain);
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


        private void crtCsrFile_Click(object sender, EventArgs e)
        {
            createPrivateKeyBtn.Visible = false;
            string path = SaveCsrFile(myConsole, myRequest);

            if (!string.IsNullOrWhiteSpace(path))
            {
                dialogResult = MessageBox.Show(messages.csrCreated + " ", "",MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    tabControl1.SelectTab(tabPage3);
                    lb_clickPrivateKeyBtn.Text = "";
                    lbl_selectedCsrFile.Text = "";
                    lbl_info_private_key.Text = messages.privateKeyMessage;
                }
                else
                {
                    Application.Exit();
                }
            }
        }


        private string SaveCsrFile(CertreqConsole myConsole, CertificateRequest myRequest)
        {
            string path = null;
            if (GetSpecialCharacter(myRequest).Count <= 0)
            {
                // Define Settings for SaveFileDialog
                SaveFileDialog saveFileDialog = SaveDialogSettings(myRequest);
                // Returns path for Console Class
                CertificateHandler myCerHandler = new CertificateHandler();
                path = CreateCsrFile(myRequest, saveFileDialog, myCerHandler);
            }
            return path;
        }


        private void openCsrFileBtn_Click(object sender, EventArgs e)
        {
            string selectedFileName = GetFileName(OpenFileDiaglog());

            if (!String.IsNullOrWhiteSpace(selectedFileName))
            {
                lbl_selectedCsrFile.Text = selectedFileName;
                lb_clickPrivateKeyBtn.Text = messages.crtPrivateKey;
                createPrivateKeyBtn.Visible = true;
            }
            
        }

        
        private void createPrivateKeyBtn_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(lbl_selectedCsrFile.Text) + "/" + Path.GetFileNameWithoutExtension(lbl_selectedCsrFile.Text);
            // creating the private key
            myConsole.SubmitCertificate(path);
            if (File.Exists(path + ".cer"))
            {
                dialogResult = MessageBox.Show(messages.installCertificate + " ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    // installing the certificate
                    myConsole.AcceptCertificate(path);
                    //tabControl1.SelectTab(tabPage4);
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show(messages.certificateFileNotCreated);
            }
        }


        private OpenFileDialog OpenFileDiaglog()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            return openFileDialog1;
        }


        private string GetFileName(OpenFileDialog openFileDialog1) {
            string selectedFileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog1.FileName;
            }
            return selectedFileName;
        }
    }
}

