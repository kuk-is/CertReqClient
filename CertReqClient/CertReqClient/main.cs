using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertReqClient
{
    class main
    {
        public static string PemEncodeSigningRequest(CertificateRequest request, PkcsSignatureGenerator generator)
        {
            byte[] pkcs10 = request.CreateSigningRequest(generator);
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("-----BEGIN CERTIFICATE REQUEST-----");

            string base64 = Convert.ToBase64String(pkcs10);

            int offset = 0;
            const int LineLength = 64;

            while (offset < base64.Length)
            {
                int lineEnd = Math.Min(offset + LineLength, base64.Length);
                builder.AppendLine(base64.Substring(offset, lineEnd - offset));
                offset = lineEnd;
            }

            builder.AppendLine("-----END CERTIFICATE REQUEST-----");
            return builder.ToString();
        }

    }

    public class PkcsSignatureGenerator
    {
    }

    public class CertificateRequest
    {
        internal byte[] CreateSigningRequest(PkcsSignatureGenerator generator)
        {
            throw new NotImplementedException();
        }
    }
}
