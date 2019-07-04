using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            string commonName = textBox_commonName.Text;
            string SubjectAlternativeNames = textbox_alternativeNames.Text;
            string Organization = textbox_organization.Text;
            string Department = textBox_Department.Text;
            string City = textBox_City.Text;
            string State = textBox_State.Text;
            string Country = textBox_Country.Text;
            string Keysize = textBox_Keysize.Text;

            
            var subjectName = "CN=www.copanyName.com,O=Company Name,OU=Department,T=Area,ST=State,C=Country";

            // create new Object for Issuer and Subject
            var issuer = new X509Name(subjectName);
            var subject = new X509Name(subjectName);

            // generate the key Value Pair, which in our case is a public Key
            var randomGenerator = new CryptoApiRandomGenerator();
            var random = new SecureRandom(randomGenerator);
            AsymmetricCipherKeyPair subjectKeyPair = default(AsymmetricCipherKeyPair);
            const int strength = 2048;
            var keyGenerationParameters = new KeyGenerationParameters(random, strength);

            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            subjectKeyPair = keyPairGenerator.GenerateKeyPair();
            AsymmetricCipherKeyPair issuerKeyPair = subjectKeyPair;

            // PKCS #10 Certificate Signing Request
            Pkcs10CertificationRequest csr = new Pkcs10CertificationRequest("SHA1WITHRSA", subject, issuerKeyPair.Public, null, issuerKeyPair.Private);

            // convert BouncyCastle CSR to .PEM file.
            StringBuilder CSRPem = new StringBuilder();
            PemWriter CSRPemWriter = new PemWriter(new StringWriter(CSRPem));
            CSRPemWriter.WriteObject(csr);
            CSRPemWriter.Writer.Flush();

            // get CSR text
            var CSRtext = CSRPem.ToString();

            // write content into text file
            using (StreamWriter f = new StreamWriter(@"C:\Cert_TEST\DemoCSR.txt"))
            {
                f.Write(CSRtext);
            }
        }
    }
}
