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
            Pro.StartInfo.Arguments = string.Format("-submit -attrib \"CertificateTemplate: webserver\" {0}.txt {0}.cer  {0}.pfx", path);
            
            Pro.Start();
            Pro.WaitForExit();

            Console.ReadLine();
        }

        // running CL and installing certificate
        public void AcceptCertificate(string path) {
            Process P = new Process();

            P.StartInfo.FileName = "certreq.exe";
            P.StartInfo.Arguments = string.Format("-accept {0}.cer", path);

            P.Start();
            P.WaitForExit();

            Console.ReadLine();
        }
    }
}
