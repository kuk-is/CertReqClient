﻿using CERTENROLLLib;
using System;
using System.IO;
using System.Text;

namespace CertReqClient
{
    class CertificateRequest
    {
        // declaring variables 
        private string commonName;
        private string subjectAlternativeNames;
        private string organization;
        private string department;
        private string city;
        private string state;
        private string country;
        private string dnsName;

        // get Methode for commonName
        public string getCommonName() {

            return commonName;
        }

        public void SetDnsName(string dnsName) {

            this.dnsName = dnsName;
        }

        // creating setter methods
        public void SetCommonName(string commonName)
        {
            this.commonName = commonName;
        }

        public void SetSubjectAlternativeNames(string subjectAlternativeNames)
        {
            this.subjectAlternativeNames = subjectAlternativeNames;
        }

        public void SetOrganization(string organization)
        {
            this.organization = organization;
        }

        public void SetDepartment(string department)
        {
            this.department = department;
        }

        public void SetCity(string city)
        {
            this.city = city;
        }

        public void SetState(string state)
        {
            this.state = state;
        }

        public void SetCountry(string country)
        {
            this.country = country;
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

            var objObjectId = new CObjectId();
            var objObjectIds = new CObjectIds();
            var objX509ExtensionEnhancedKeyUsage = new CX509ExtensionEnhancedKeyUsage();
            objObjectId.InitializeFromValue("1.3.6.1.5.5.7.3.2");
            objObjectIds.Add(objObjectId);
            objX509ExtensionEnhancedKeyUsage.InitializeEncode(objObjectIds);
            objPkcs10.X509Extensions.Add((CX509Extension)objX509ExtensionEnhancedKeyUsage);

            var objDN = new CX500DistinguishedName();
            var subjectName = "CN=" + commonName + ", OU=" + department + ", O=" + organization + ", L=" + city + ", S=" + state + ", C=" + country;
        
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
