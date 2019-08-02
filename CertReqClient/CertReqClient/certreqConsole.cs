using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertReqClient
{
    class CertreqConsole
    {
        // running CL and submitting certificate
        public void SubmitCertificate(string filename, string path) {
            /*
            Process Pro = new Process();

            Pro.StartInfo.FileName = "certreq.exe";
            Pro.StartInfo.Arguments = "-submit -attrib \"CertificateTemplate: webserver\" C:/Cert_TEST/newCertificate.txt C:/Cert_TEST/newCertificate.cer C:/Cert_TEST/newCertificate.pfx";
            Pro.Start();
            Pro.WaitForExit();

            Console.ReadLine();
            */

            Process Pro = new Process();

            Pro.StartInfo.FileName = "certreq.exe";
            Pro.StartInfo.Arguments = "-submit -attrib \"CertificateTemplate: webserver\""  + filename + " "  + path + ".cer " + " " + path + ".pfx";
            
            Pro.Start();
            Pro.WaitForExit();

            Console.ReadLine();
        }

        // running CL and installing certificate
        public void AcceptCertificate(string path) {
            /*
            Process P = new Process();

            P.StartInfo.FileName = "certreq.exe";
            P.StartInfo.Arguments = "-accept C:/Cert_TEST/newCertificate.cer";
            P.Start();

            Console.ReadLine();
            */

            Process P = new Process();

            P.StartInfo.FileName = "certreq.exe";
            P.StartInfo.Arguments = "-accept " + path + ".cer";
            P.Start();

            Console.ReadLine();
        }
    }
}
