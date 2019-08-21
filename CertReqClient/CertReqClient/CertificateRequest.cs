using CERTENROLLLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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

        
        
        public IEnumerable<string> SubjectAlternativeDnsNames
        {
           get { return this.SubjectAlternativeNames.Where(s => s.StartsWith("dns=")); }
        }

        public IEnumerable<string> SubjectAlternativeIpAddresses
        {
            get { return this.SubjectAlternativeNames.Where(s => s.StartsWith("ipaddress=")).Select(s => s.Replace("ipaddress=", string.Empty)); }
        }

        public string[] SubjectAlternativeNames
        {
            get { return this.SubjectAlternativeName.Split(new string[] { "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries); }
        }
        
   

      
        // code for encrypting the request
        public string GenerateCertificateRequest()
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


            //////////////////////////////////////////////////////////////
            ////////////// SUBJECT ALTERNATIVE NAME (SAN) ///////////////
            ////////////////////////////////////////////////////////////

            if (SubjectAlternativeNames.Any())
            {
                CAlternativeNames objAlternativeNames = new CAlternativeNames();
                CX509ExtensionAlternativeNames objExtensionAlternativeNames = new CX509ExtensionAlternativeNames();

                foreach (var sanName in this.SubjectAlternativeNames)
                {
                    CAlternativeName objRfc822Name = new CAlternativeName();

                    if (sanName.ToLowerInvariant().StartsWith("dns"))
                    {
                        // Set Alternative DNS Name
                        string cleanedName = sanName.Replace("dns=", string.Empty);
                        objRfc822Name.InitializeFromString(AlternativeNameType.XCN_CERT_ALT_NAME_DNS_NAME, cleanedName);
                        objAlternativeNames.Add(objRfc822Name);
                    }
                    else if (sanName.ToLowerInvariant().StartsWith("ipaddress"))
                    {
                        // Set Alternative IP-Adress
                        string cleanedName = sanName.Replace("ipaddress=", string.Empty);
                        IPAddress ipAddress;
                        if (IPAddress.TryParse(cleanedName, out ipAddress))
                        {
                            string ipBase64 = Convert.ToBase64String(ipAddress.GetAddressBytes());
                            objRfc822Name.InitializeFromRawData(AlternativeNameType.XCN_CERT_ALT_NAME_IP_ADDRESS, EncodingType.XCN_CRYPT_STRING_BASE64, ipBase64);
                            objAlternativeNames.Add(objRfc822Name);
                        }
                    }
                }

                objExtensionAlternativeNames.InitializeEncode(objAlternativeNames);
                objPkcs10.X509Extensions.Add((CX509Extension)objExtensionAlternativeNames);
            }

            //////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////


            var objObjectId = new CObjectId();
            var objObjectIds = new CObjectIds();
            var objX509ExtensionEnhancedKeyUsage = new CX509ExtensionEnhancedKeyUsage();
            objObjectId.InitializeFromValue("1.3.6.1.5.5.7.3.2");
            objObjectIds.Add(objObjectId);
            objX509ExtensionEnhancedKeyUsage.InitializeEncode(objObjectIds);
            objPkcs10.X509Extensions.Add((CX509Extension)objX509ExtensionEnhancedKeyUsage);

            var objDN = new CX500DistinguishedName();
            var subjectName = "CN=" + this.CommonName + ", OU=" + this.Department + ", O=" + this.Organization + ", L=" + this.City + ", S=" + this.State + ", C=" + this.Country;

            objDN.Encode(subjectName, X500NameFlags.XCN_CERT_NAME_STR_NONE);
            objPkcs10.Subject = objDN;

            var objEnroll = new CX509Enrollment();
            objEnroll.InitializeFromRequest(objPkcs10);
            var strRequest = objEnroll.CreateRequest(EncodingType.XCN_CRYPT_STRING_BASE64);

            string certBegin = "-----BEGIN CERTIFICATE REQUEST-----\r\n";
            string certEnd = "-----END CERTIFICATE REQUEST-----";
            strRequest = certBegin + strRequest + certEnd;

            return strRequest;
        }
    }
}
