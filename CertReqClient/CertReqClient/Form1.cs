using CERTENROLLLib;
using System;
using System.Collections.Generic;
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
        List<string> list = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {


            // filling the combobox
            comboBox_country.DisplayMember = "Text";
            comboBox_country.ValueMember = "Value";

            IEnumerable<CultureInfo> cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.Name);
                comboBox_country.Items.Add(new { Text = $"{region.EnglishName} ({region.TwoLetterISORegionName})", Value = region.TwoLetterISORegionName });

                list.Add(comboBox_country.SelectedValue.ToString());
            }
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
            myRequest.SetCountry(textBox_Country.Text);

            // calling method to create request file
            myRequest.CreateRequestFile(myRequest.GenerateCertificateRequest());


            /////////////////////// CONSOLE ////////////////////////
            CertreqConsole myConsole = new CertreqConsole();

            // calling method for console commands
            myConsole.SubmitCertificate();
            myConsole.AcceptCertificate();

        }
    }
}

