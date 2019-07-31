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
            //label_country.Text = comboBox_country.SelectedValue.ToString();
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
            //myRequest.SetCountry(textBox_Country.Text);
            myRequest.SetCountry(comboBox_country.SelectedValue.ToString());


            // calling method to create request file
            myRequest.CreateRequestFile(myRequest.GenerateCertificateRequest());


            /////////////////////// CONSOLE ////////////////////////
            CertreqConsole myConsole = new CertreqConsole();

            // calling method for console commands
            myConsole.SubmitCertificate();
            myConsole.AcceptCertificate();

            //string folderPath = @"C:\Cert_TEST"; // Your path Where you want to save other than Server.MapPath   
            /*
            string testikus = comboBox_country.SelectedValue.ToString();
            */
        }
    }
}

