using CERTENROLLLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
            // filling the combobox
            comboBox_country.DisplayMember = "Value";
            comboBox_country.ValueMember = "Key";


            IEnumerable<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures).OrderBy(culture => culture.EnglishName);

            Dictionary<string, string> countries = new Dictionary<string, string>(); //int i = 0;
            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.Name);
                if (countries.ContainsKey(region.TwoLetterISORegionName) == false)
                {
                    countries.Add(region.TwoLetterISORegionName, $"{region.EnglishName}");
                }
            }

            comboBox_country.DataSource = new BindingSource(countries, null);
            comboBox_country.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        

        private void Btn_generate_Click(object sender, EventArgs e)
        {
            CertificateRequest myRequest = new CertificateRequest();

            myRequest.SetCommonName(textBox_commonName.Text);
            myRequest.SetSubjectAlternativeNames(textbox_alternativeNames.Text);
            myRequest.SetOrganization(textbox_organization.Text);
            myRequest.SetDepartment(textBox_Department.Text);
            myRequest.SetCity(textBox_City.Text);
            myRequest.SetState(textBox_State.Text);
            myRequest.SetCountry(comboBox_country.SelectedValue.ToString());
            


            // saving CSR file to specific folder
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FileName = myRequest.GetCommonName() + ".txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            
           

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Code to write the stream goes here.
                string filename = saveFileDialog1.FileName;
                var request = myRequest.GenerateCertificateRequest();
                
                File.WriteAllText(filename, request);

                // create full path for console commands
                string path = Path.GetDirectoryName(filename)  + "/"  +  Path.GetFileNameWithoutExtension(filename);
                
                
                /////////////////////// CONSOLE ////////////////////////
                CertreqConsole myConsole = new CertreqConsole();

                // calling method for console commands
                myConsole.SubmitCertificate(path);
                myConsole.AcceptCertificate(path);

            }
        }
    }
}

