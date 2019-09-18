using System;


namespace CertReqClient
{
    class CertificateRequest
    {
        public string CommonName { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string SubjectAlternativeName { get; set; }


        public string[] SubjectAlternativeNames
        {
            get
            {
                if (string.IsNullOrEmpty(this.SubjectAlternativeName))
                {
                    return new string[0];
                }

                return this.SubjectAlternativeName.Split(new string[] { "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }     
    }
}
