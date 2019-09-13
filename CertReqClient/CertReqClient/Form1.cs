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

        private void Form1_Load(object sender, EventArgs e)
        {
            // Calling FillCountryDropDown() Method
            FillCountryDropDown();

            subjectType.Items.Add(messages.dnsName);
            subjectType.Items.Add(messages.ipAddress);

            // enable textbox by default
            textbox_alternativeNames.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"><see cref="btn_generate"/></param>
        /// <param name="e">event arguments</param>
        private void Btn_generate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox_commonName.Text) && (!String.IsNullOrWhiteSpace(textBox_commonName.Text)))
            {
                // Read all Input Fields
                CertificateRequest myRequest = ReadInputValues();

                IEnumerable<string> specialCharacters = GetSpecialCharacter(myRequest);
                if (specialCharacters.Count() <= 0)
                {
                    string path = SaveCsrFile(myConsole, myRequest);
                    // calling method for console commands
                    myConsole.SubmitCertificate(path);
                    myConsole.AcceptCertificate(path);
                    // Final Page messages
                    finalPageMessage();
                    // switch to next Tab
                    goToNextPage("tabPage4");
                }
                else
                {
                    string specialChar = string.Join("", specialCharacters);
                    MessageBox.Show(string.Format(messages.charactersNotAllowed, specialChar));
                }
            }
            else
            {
                MessageBox.Show(messages.enterDomain);
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

        private SaveFileDialog SaveDialogSettings(string filename)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FileName = filename + ".txt";
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
                // Create Request File
                CreateFile(myRequest, myCerHandler, filename);

                // create full path for console commands
                string path = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));

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
                switch (subjectType.SelectedIndex)
                {
                    case 0: // DNS-Name
                        subjectTypeName = messages.dnsEquals;
                        break;
                    case 1: // IP-Address
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

        private HashSet<string> GetSpecialCharacter(CertificateRequest myRequest)
        {
            string[] specialChars = new string[] { "+", ",", "\"", ";"};
            HashSet<string> charsIn = new HashSet<string>();
            string[] inputFields = new string[]
            {
                myRequest.Organization,
                myRequest.CommonName,
                myRequest.Department,
                myRequest.City,
                myRequest.State,
                myRequest.Country
            };

            foreach (string specialChar in specialChars)
            {
                foreach (string field in inputFields)
                {
                    if (field.Contains(specialChar))
                    {
                        if (!charsIn.Contains(specialChar))
                        {
                            charsIn.Add(specialChar);
                        }
                    }
                }
            }
            return charsIn;
        }


        private void editSAN_Click(object sender, EventArgs e)
        {
            editSAN.Text = textbox_alternativeNames.Enabled ? messages.editSan : messages.closeEditMode;
            textbox_alternativeNames.Enabled = !textbox_alternativeNames.Enabled;
        }

        private void generateCsrBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox_commonName.Text))
            {
                CertificateRequest myRequest = ReadInputValues();
                IEnumerable<string> specialCharacters = GetSpecialCharacter(myRequest);

                if (specialCharacters.Count() <= 0)
                {
                    // switch to next Tab
                    goToNextPage("tabPage2");
                    SetDataForOverview();
                }
                else
                {
                    string specialChar = string.Join("", specialCharacters);
                    MessageBox.Show(messages.charactersNotAllowed, specialChar);
                }
            }
            else
            {
                MessageBox.Show(messages.enterDomain);
            }
        }

        private void SetDataForOverview()
        {
            CertificateRequest myRequest = ReadInputValues();

            tb_subAltNames.ScrollBars = ScrollBars.Both;
            tb_subAltNames.WordWrap = false;
            tb_subAltNames.ReadOnly = true;

            // set textboxes to ReadOnly
            SetTbReadOnly();

            tb_overview_domain.Text = myRequest.CommonName;
            tb_subAltNames.Text = myRequest.SubjectAlternativeName;
            tb_overview_organization.Text = myRequest.Organization;
            tb_overview_department.Text = myRequest.Department;
            tb_overview_city.Text = myRequest.City;
            tb_overview_state.Text = myRequest.State;
            
            tb_overview_country.Text = ((KeyValuePair<string, string>)comboBox_country.SelectedItem).Value;
        }


        private void overviewBackBtn_Click(object sender, EventArgs e)
        {
            // switch to next Tab
            goToNextPage("tabPage1");
        }

        private void crtCsrFile_Click(object sender, EventArgs e)
        {
            createPrivateKeyBtn.Visible = false;
            CertificateRequest myRequest = ReadInputValues();
            string path = SaveCsrFile(myConsole, myRequest);

            if (!string.IsNullOrWhiteSpace(path))
            {
                DialogResult dialogResult = MessageBox.Show(messages.csrCreated, "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    // switch to next Tab
                    goToNextPage("tabPage3");
                    string privateInfoTxt = "Your file has been successfully created. You can now choose the right certificate \r request file (csr) to generate the private key.";
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
            if (GetSpecialCharacter(myRequest).Count() <= 0)
            {
                // Define Settings for SaveFileDialog
                SaveFileDialog saveFileDialog = SaveDialogSettings(myRequest.CommonName);

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
            string path = Path.Combine(Path.GetDirectoryName(lbl_selectedCsrFile.Text), Path.GetFileNameWithoutExtension(lbl_selectedCsrFile.Text));
            // creating the private key
            myConsole.SubmitCertificate(path);
            if (File.Exists(path + ".cer"))
            {
                DialogResult dialogResult = MessageBox.Show(messages.installCertificate, "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    // installing the certificate
                    myConsole.AcceptCertificate(path);
                    // Final Page messages
                    finalPageMessage();
                    // switch to next Tab
                    goToNextPage("tabPage4");
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

        private string GetFileName(OpenFileDialog openFileDialog1)
        {
            string selectedFileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog1.FileName;
            }

            return selectedFileName;
        }

        private void SetTbReadOnly()
        {
            tb_overview_domain.Enabled = false;
            tb_overview_organization.Enabled = false;
            tb_overview_department.Enabled = false;
            tb_overview_city.Enabled = false;
            tb_overview_state.Enabled = false;
            tb_overview_country.Enabled = false;
        }


        private void goToNextPage(string tabNumber)
        {
            tabControl1.SelectTab(tabNumber);
        }


        private void finalPageMessage() {

            string installFinished = "Installation Completed!";
            string finalPageMessage = "Your certificate has been successfully installed. \nYou can now close the installation wizard.";
            finalPageTxt.Text = finalPageMessage;
            lbl_installCompleted.Text = installFinished;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CreateFile(CertificateRequest myRequest, CertificateHandler myCerHandler, string filename)
        {
            var request = myCerHandler.GenerateSigningRequest(myRequest);
            File.WriteAllText(filename, request);
        }
    }
}

        