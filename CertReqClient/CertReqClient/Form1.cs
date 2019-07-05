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

            // 
            myRequest.setCommonName(textBox_commonName.Text);
            myRequest.setCommonName(textbox_alternativeNames.Text);
            myRequest.setCommonName(textbox_organization.Text);
            myRequest.setCommonName(textBox_Department.Text);
            myRequest.setCommonName(textBox_City.Text);
            myRequest.setCommonName(textBox_State.Text);
            myRequest.setCommonName(textBox_Country.Text);

            string certBegin = "-----BEGIN CERTIFICATE REQUEST-----\r\n";
            string certEnd = "-----END CERTIFICATE REQUEST-----";
            string fullRequest = certBegin + myRequest.generateCertificateRequest() + certEnd;

            // create text file and save in folder
            string path = @"c:\Cert_TEST\myCerti.txt";
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(fullRequest);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
    }
}

