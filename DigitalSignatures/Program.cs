using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using System;
using System.Drawing;

namespace DigitalSignatures
{
    class Program
    {
        static void Main()
        {
            // Load the PDF document
            Document pdfDoc = new Document("C:\\Users\\DELL\\OneDrive\\MyComputer\\PublicAndPrivateKey\\DigitalSignatures\\DigitalSignatures\\DigitalSignatures\\AAA.pdf");

            // Specify the name of the field you want to find
            string fieldName = "*Created By Yolo Team";

            // Create TextFragmentAbsorber object to search text
            TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(fieldName);

            // Accept the absorber for all pages
            pdfDoc.Pages.Accept(textFragmentAbsorber);

            TextFragmentCollection textFragments = textFragmentAbsorber.TextFragments;
            
            Console.WriteLine(textFragments.Count);

            if (textFragments.Count > 0)
            {
                TextFragment firstTextFragment = textFragments[1];

                // Get the position of the field
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                    Convert.ToInt32(firstTextFragment.Rectangle.LLX),
                    Convert.ToInt32(firstTextFragment.Rectangle.LLY - 100),
                    Convert.ToInt32(firstTextFragment.Rectangle.Width + 1000),
                    Convert.ToInt32(firstTextFragment.Rectangle.Height) + 88);

                

                Console.WriteLine($"{Convert.ToInt32(firstTextFragment.Rectangle.Width)}-{Convert.ToInt32(firstTextFragment.Rectangle.Height)}");

                // Instantiate the PdfFileSignature for the loaded PDF document
                Aspose.Pdf.Facades.PdfFileSignature pdfSignature = new Aspose.Pdf.Facades.PdfFileSignature(pdfDoc);

                // Load the certificate file along with the password
                PKCS7 pkcs = new PKCS7("C:\\Storage\\YOLO\\YOLO-Certificates\\1b8444db-6053-4ef1-a4d2-0fb799d3db77\\1b8444db-6053-4ef1-a4d2-0fb799d3db77.pfx", "24062001");

                // Assign the access permissions
                DocMDPSignature docMdpSignature = new DocMDPSignature(pkcs, DocMDPAccessPermissions.FillingInForms);

                // Sign the PDF file with the certify method
                pdfSignature.Certify(1, "Sign Plan", "0834102453", "Binh-Dinh", true, rect, docMdpSignature);

                // Set the certificate
                pdfSignature.SetCertificate("C:\\Storage\\YOLO\\YOLO-Certificates\\1b8444db-6053-4ef1-a4d2-0fb799d3db77\\1b8444db-6053-4ef1-a4d2-0fb799d3db77.pfx", "24062001");

                // Save digitally signed PDF file 
                var fileName = $"DigitallySignedPDF -{Guid.NewGuid()}.pdf";
                pdfSignature.Save(fileName);

                Console.WriteLine("The PDF file has been digitally signed.");

                // Check if the created file contains a signature
                Aspose.Pdf.Facades.PdfFileSignature pdfSignatureTest = new Aspose.Pdf.Facades.PdfFileSignature(fileName);

                if (pdfSignatureTest.ContainsSignature())
                {
                    Console.WriteLine("The created PDF file contains a signature.");
                    foreach(var item in pdfSignatureTest.GetSignNames())
                    {
                        Console.WriteLine(item);
                        Console.WriteLine("-: " + pdfSignatureTest.ExtractCertificate(item).ToString());
                    }
                    
                }
            }
            

            Console.WriteLine("Done");
        }
    }
}
