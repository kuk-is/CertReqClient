using System;
using System.Linq;
using System.Net;
using CERTENROLLLib;

namespace CertReqClient
{
    class CertificateHandler
    {
        
        public string GenerateSigningRequest(CertificateRequest myRequest)
        {

            CCspInformations objCSPs = new CCspInformations();
            objCSPs.AddAvailableCsps();

            CX509PrivateKey objPrivateKey = CreatePrivateKey(objCSPs);
            CX509CertificateRequestPkcs10 objPkcs10 = CreateCertificateRequest(objPrivateKey);
            CX509CertificateRequestPkcs10 keyUsage = ExtensionKeyUsage(objPkcs10);
            CX509CertificateRequestPkcs10 multipleSan = MultipleSan(keyUsage, myRequest);
            CX509CertificateRequestPkcs10 objObjectId = ObjObjectId(multipleSan);
            CX509CertificateRequestPkcs10 createCsrSubject = CreateCsrSubject(myRequest, keyUsage);
            var strRequest = StrRequest(createCsrSubject);

            return strRequest;
        }

                                 
        private static CX509CertificateRequestPkcs10 CreateCertificateRequest(CX509PrivateKey objPrivateKey)
        {
            CX509CertificateRequestPkcs10 objPkcs10 = new CX509CertificateRequestPkcs10();
            objPkcs10.InitializeFromPrivateKey(
                X509CertificateEnrollmentContext.ContextUser,
                objPrivateKey,
                string.Empty);
            return objPkcs10;
        }


        private static CX509PrivateKey CreatePrivateKey(CCspInformations objCSPs)
        {
            CX509PrivateKey objPrivateKey = new CX509PrivateKey();
            objPrivateKey.Length = 2048;
            objPrivateKey.KeySpec = X509KeySpec.XCN_AT_SIGNATURE;
            objPrivateKey.KeyUsage = X509PrivateKeyUsageFlags.XCN_NCRYPT_ALLOW_ALL_USAGES;
            objPrivateKey.MachineContext = false;
            objPrivateKey.ExportPolicy = X509PrivateKeyExportFlags.XCN_NCRYPT_ALLOW_EXPORT_FLAG;
            objPrivateKey.CspInformations = objCSPs;
            objPrivateKey.Create();
            return objPrivateKey;
        }


        private CX509CertificateRequestPkcs10 ExtensionKeyUsage(CX509CertificateRequestPkcs10 objPkcs10)
        {
            CX509ExtensionKeyUsage objExtensionKeyUsage = new CX509ExtensionKeyUsage();
            objExtensionKeyUsage.InitializeEncode(
                CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_DIGITAL_SIGNATURE_KEY_USAGE |
                CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_NON_REPUDIATION_KEY_USAGE |
                CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_KEY_ENCIPHERMENT_KEY_USAGE |
                CERTENROLLLib.X509KeyUsageFlags.XCN_CERT_DATA_ENCIPHERMENT_KEY_USAGE);
            objPkcs10.X509Extensions.Add((CX509Extension)objExtensionKeyUsage);
            return objPkcs10;
        }

        
        private CX509CertificateRequestPkcs10 MultipleSan(CX509CertificateRequestPkcs10 objPkcs10, CertificateRequest myRequest) {

            if (myRequest.SubjectAlternativeNames.Any())
            {
                CAlternativeNames objAlternativeNames = new CAlternativeNames();
                CX509ExtensionAlternativeNames objExtensionAlternativeNames = new CX509ExtensionAlternativeNames();

                foreach (string sanName in myRequest.SubjectAlternativeNames)
                {
                    CAlternativeName objRfc822Name = new CAlternativeName();

                    
                    if (sanName.StartsWith("dns", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Set Alternative DNS Name
                        string cleanedName = sanName.Replace(messages.dnsEquals, string.Empty);
                        objRfc822Name.InitializeFromString(AlternativeNameType.XCN_CERT_ALT_NAME_DNS_NAME, cleanedName);
                        objAlternativeNames.Add(objRfc822Name);
                    }
                    else if (sanName.StartsWith("ipaddress", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Set Alternative IP-Adress
                        string cleanedName = sanName.Replace(messages.ipaddressEquals, string.Empty);
                        if (IPAddress.TryParse(cleanedName, out IPAddress ipAddress))
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
            return objPkcs10;
        }


        private CX509CertificateRequestPkcs10 ObjObjectId(CX509CertificateRequestPkcs10 objPkcs10)
        {
            CObjectId objObjectId = new CObjectId();
            CObjectIds objObjectIds = new CObjectIds();
            CX509ExtensionEnhancedKeyUsage objX509ExtensionEnhancedKeyUsage = new CX509ExtensionEnhancedKeyUsage();
            objObjectId.InitializeFromValue("1.3.6.1.5.5.7.3.2");
            objObjectIds.Add(objObjectId);
            objX509ExtensionEnhancedKeyUsage.InitializeEncode(objObjectIds);
            objPkcs10.X509Extensions.Add((CX509Extension)objX509ExtensionEnhancedKeyUsage);

            return objPkcs10;
        }


        private CX509CertificateRequestPkcs10 CreateCsrSubject(CertificateRequest myRequest, CX509CertificateRequestPkcs10 objPkcs10)
        {
            CX500DistinguishedName objDN = new CX500DistinguishedName();
            string subjectName = 
                  "CN=" + myRequest.CommonName + 
                 ",OU=" + myRequest.Department + 
                  ",O=" + myRequest.Organization + 
                  ",L=" + myRequest.City + 
                  ",S=" + myRequest.State + 
                  ",C=" + myRequest.Country;

            objDN.Encode(subjectName, X500NameFlags.XCN_CERT_NAME_STR_NONE);
            objPkcs10.Subject = objDN;

            return objPkcs10;
        }


        private string StrRequest(CX509CertificateRequestPkcs10 objPkcs10) {

            CX509Enrollment objEnroll = new CX509Enrollment();
            objEnroll.InitializeFromRequest(objPkcs10);
            string strRequest = objEnroll.CreateRequest(EncodingType.XCN_CRYPT_STRING_BASE64);

            string certBegin = "-----BEGIN CERTIFICATE REQUEST-----\r\n";
            string certEnd = "-----END CERTIFICATE REQUEST-----";
            strRequest = certBegin + strRequest + certEnd;

            return strRequest;
        }
    }
}
