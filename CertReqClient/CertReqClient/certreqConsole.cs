using System;
using System.Diagnostics;

namespace CertReqClient
{
    class CertreqConsole
    {
        // running CL and creating request file
        public void CreateInfCommand(string path)
        {
            Process ProNew = new Process();

            ProNew.StartInfo.FileName = "certreq.exe";
            ProNew.StartInfo.Arguments = string.Format("-new {0}.inf {0}.txt", path);

            ProNew.Start();
            ProNew.WaitForExit();

            Console.ReadLine();
        }

        // running CL and submitting certificate
        public void SubmitCertificate(string path) {
            Process ProSubmit = new Process();

            ProSubmit.StartInfo.FileName = "certreq.exe";
            ProSubmit.StartInfo.Arguments = string.Format("-submit -attrib \"CertificateTemplate: webserver\" {0}.txt {0}.cer  {0}.pfx", path);

            ProSubmit.Start();
            ProSubmit.WaitForExit();

            Console.ReadLine();
        }

        // running CL and installing certificate
        public void AcceptCertificate(string path) {
            Process ProAccept = new Process();

            ProAccept.StartInfo.FileName = "certreq.exe";
            ProAccept.StartInfo.Arguments = string.Format("-accept {0}.cer", path);

            ProAccept.Start();
            ProAccept.WaitForExit();

            Console.ReadLine();
        }
    }
}
