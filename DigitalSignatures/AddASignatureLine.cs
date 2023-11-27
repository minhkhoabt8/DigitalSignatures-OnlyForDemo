using Aspose.Words.DigitalSignatures;
using Aspose.Words.Drawing;
using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignatures
{
    public class SignatureManager
    {
        // Assuming ArtifactsDir and MyDir are defined elsewhere in your code.
        private static string ArtifactsDir = "path/to/artifacts/";
        private static string MyDir = "path/to/certificates/";

        public static void AddAndSignSignatureLine()
        {
            // Create a new document and document builder
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            // Set signature line options
            SignatureLineOptions signatureLineOptions = GetDefaultSignatureLineOptions();

            // Insert signature line into the document
            SignatureLine signatureLine = builder.InsertSignatureLine(signatureLineOptions).SignatureLine;
            signatureLine.ProviderId = Guid.Parse("CF5A7BB4-8F3C-4756-9DF6-BEF7F13259A2");

            // Save the document with the inserted signature line
            string documentPath = ArtifactsDir + "SignDocuments.SignatureLineProviderId.docx";
            doc.Save(documentPath);

            // Set sign options and sign the document
            SignOptions signOptions = GetSignOptions(signatureLine);
            CertificateHolder certHolder = CertificateHolder.Create(MyDir + "morzal.pfx", "aw");

            DigitalSignatureUtil.Sign(documentPath,
                ArtifactsDir + "SignDocuments.CreateNewSignatureLineAndSetProviderId.docx", certHolder, signOptions);
        }

        private static SignatureLineOptions GetDefaultSignatureLineOptions()
        {
            return new SignatureLineOptions
            {
                Signer = "yourname",
                SignerTitle = "Worker",
                Email = "yourname@aspose.com",
                ShowDate = true,
                DefaultInstructions = false,
                Instructions = "Please sign here.",
                AllowComments = true
            };
        }

        private static SignOptions GetSignOptions(SignatureLine signatureLine)
        {
            return new SignOptions
            {
                SignatureLineId = signatureLine.Id,
                ProviderId = signatureLine.ProviderId,
                Comments = "Document was signed by Aspose",
                SignTime = DateTime.Now
            };
        }
    }
}
