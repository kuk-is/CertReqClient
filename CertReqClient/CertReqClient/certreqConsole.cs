using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertReqClient
{
    class certreqConsole
    {
        public void submitCertificate() {

            Process Pro = new Process();

            Pro.StartInfo.FileName = "certreq.exe";
            Pro.StartInfo.Arguments = "-submit -attrib \"CertificateTemplate: webserver\" C:/Cert_TEST/newCertificate.txt C:/Cert_TEST/newCertificate.cer C:/Cert_TEST/newCertificate.pfx";
            Pro.Start();
            Pro.WaitForExit();

            Console.ReadLine();
        }

        public void acceptCertificate() { 

            Process P = new Process();

            P.StartInfo.FileName = "certreq.exe";
            P.StartInfo.Arguments = "-accept C:/Cert_TEST/newCertificate.cer";
            P.Start();

            Console.ReadLine();
        }
    }
}
