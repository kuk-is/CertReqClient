using CERTENROLLLib;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CertReqClient
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            CertificateRequest myRequest = new CertificateRequest();

            myRequest.setCommonName(textBox_commonName.Text);
            myRequest.setSubjectAlternativeNames(textbox_alternativeNames.Text);
            myRequest.setOrganization(textbox_organization.Text);
            myRequest.setDepartment(textBox_Department.Text);
            myRequest.setCity(textBox_City.Text);
            myRequest.setState(textBox_State.Text);
            myRequest.setCountry(textBox_Country.Text);
            
            // calling method to create request file
            myRequest.createRequestFile(myRequest.generateCertificateRequest());         
        }
    }
}

