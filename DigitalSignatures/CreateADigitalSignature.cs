using Aspose.Words.DigitalSignatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignatures
{
    public class CreateADigitalSignature
    {
        /// <summary>
        /// Create A Certificate Holder To Sign Document
        /// </summary>
        /// <param name="certificatePath"></param>
        /// <param name="certificatePass"></param>
        /// <returns></returns>
        public static CertificateHolder CreateCertificate(string certificatePath, string certificatePass)
        {
            return CertificateHolder.Create(certificatePath, certificatePass);
        }
        /// <summary>
        /// Sign Document With Signed CertificateHolder
        /// </summary>
        /// <param name="documentPath"></param>
        /// <param name="destPath"></param>
        /// <param name="certHolder"></param>
        public static void SignDocument(string documentPath, string destPath, CertificateHolder certHolder)
        {
            DigitalSignatureUtil.Sign(documentPath, destPath + Path.GetFileName(documentPath),
                certHolder);
        }
    }
}
