using System;
using System.Diagnostics;

namespace CertReqClient
{
    class CertreqConsole
    {
        // running CL and submitting certificate
        public void SubmitCertificate(string path) {
            Process Pro = new Process();
                                  
            Pro.StartInfo.FileName = "certreq.exe";
            Pro.StartInfo.Arguments = "-submit -attrib \"CertificateTemplate: webserver\" "  + path + ".txt "  + path + ".cer " + " " + path + ".pfx";
            
            Pro.Start();
            Pro.WaitForExit();

            Console.ReadLine();
        }

        // running CL and installing certificate
        public void AcceptCertificate(string path) {
            Process P = new Process();

            P.StartInfo.FileName = "certreq.exe";
            P.StartInfo.Arguments = "-accept " + path + ".cer";
            P.Start();

            Console.ReadLine();
        }
    }
}
