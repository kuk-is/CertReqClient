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
            string organization = textbox_organization.Text;
            string department = textBox_Department.Text;
            string city = textBox_City.Text;
            string state = textBox_State.Text;
            string country = textBox_Country.Text;
            string Keysize = textBox_Keysize.Text;

            string CreateCertRequestMessage()
            {
                var objCSPs = new CCspInformations();
                objCSPs.AddAvailableCsps();

                var objPrivateKey = new CX509PrivateKey();
                objPrivateKey.Length = 2048;
                objPrivateKey.KeySpec = X509KeySpec.XCN_AT_SIGNATURE;
                objPrivateKey.KeyUsage = X509PrivateKeyUsageFlags.XCN_NCRYPT_ALLOW_ALL_USAGES;
                objPrivateKey.MachineContext = false;
                objPrivateKey.ExportPolicy = X509PrivateKeyExportFlags.XCN_NCRYPT_ALLOW_EXPORT_FLAG;
                objPrivateKey.CspInformations = objCSPs;
                objPrivateKey.Create();

                var objPkcs10 = new CX509CertificateRequestPkcs10();
                objPkcs10.InitializeFromPrivateKey(
                    X509CertificateEnrollmentContext.ContextUser,
                    objPrivateKey,
                    string.Empty);

                var objExtensionKeyUsage = new CX509ExtensionKeyUsage();
                objExtensionKeyUsage.InitializeEncode(
                    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_DIGITAL_SIGNATURE_KEY_USAGE |
                    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_NON_REPUDIATION_KEY_USAGE |
                    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_KEY_ENCIPHERMENT_KEY_USAGE |
                    CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_DATA_ENCIPHERMENT_KEY_USAGE);
                objPkcs10.X509Extensions.Add((CX509Extension)objExtensionKeyUsage);

                var objObjectId = new CObjectId();
                var objObjectIds = new CObjectIds();
                var objX509ExtensionEnhancedKeyUsage = new CX509ExtensionEnhancedKeyUsage();
                objObjectId.InitializeFromValue("1.3.6.1.5.5.7.3.2");
                objObjectIds.Add(objObjectId);
                objX509ExtensionEnhancedKeyUsage.InitializeEncode(objObjectIds);
                objPkcs10.X509Extensions.Add((CX509Extension)objX509ExtensionEnhancedKeyUsage);

                var objDN = new CX500DistinguishedName();
                //var subjectName = "CN = shaunxu.me, OU = ADCS, O = Blog, L = Beijng, S = Beijing, C = CN";
                var subjectName = "CN=" + commonName + ", OU=" + department + ", O=" + organization + ", L=" + city + ", S=" + state + ", C=" + country;
                objDN.Encode(subjectName, X500NameFlags.XCN_CERT_NAME_STR_NONE);
                objPkcs10.Subject = objDN;

                var objEnroll = new CX509Enrollment();
                objEnroll.InitializeFromRequest(objPkcs10);
                var strRequest = objEnroll.CreateRequest(EncodingType.XCN_CRYPT_STRING_BASE64);
                return strRequest;
            }


            // create text file and save in folder
            string path = @"c:\Cert_TEST\myCerti.txt";
            using (FileStream fs = File.Create(path))
            {
                string certBegin = "-----BEGIN CERTIFICATE REQUEST-----\r\n";
                string certEnd   = "-----END CERTIFICATE REQUEST-----";
                string fullText  = certBegin + CreateCertRequestMessage() + certEnd;

                Byte[] info = new UTF8Encoding(true).GetBytes(fullText);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
    }
}

