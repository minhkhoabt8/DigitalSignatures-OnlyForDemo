using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        // Generate RSA key pair
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
        {
            // Export private key
            string privateKey = rsa.ToXmlString(true);
            SaveToFile("1b8444db-6053-4ef1-a4d2-0fb799d3db77.key", privateKey);

            // Export public key
            string publicKey = rsa.ToXmlString(false);
            SaveToFile("1b8444db-6053-4ef1-a4d2-0fb799d3db77.crt", publicKey);

            // User information
            string userName = "John Doe";
            string userEmail = "john.doe@example.com";

            // Get or create the folder based on the user's GUID
            string userGuid = "1b8444db-6053-4ef1-a4d2-0fb799d3db77";
            string folderPath = Path.Combine("C:\\Storage\\YOLO\\YOLO-Certificates", userGuid);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine($"Created folder: {folderPath}");
            }

            // Create a self-signed certificate with user information
            X509Certificate2 certificate = CreateSelfSignedCertificate(rsa, userName, userEmail);

            // Export certificate to the folder
            string certFileName = Path.Combine(folderPath, "1b8444db-6053-4ef1-a4d2-0fb799d3db77.crt");
            SaveToFile(certFileName, certificate.Export(X509ContentType.Cert));

            // Export PFX to the folder with a password
            string password = "24062001";
            string pfxFileName = Path.Combine(folderPath, "1b8444db-6053-4ef1-a4d2-0fb799d3db77.pfx");
            byte[] pfxBytes = certificate.Export(X509ContentType.Pfx, password);
            SaveToFile(pfxFileName, pfxBytes);
        }
    }

    static X509Certificate2 CreateSelfSignedCertificate(RSACryptoServiceProvider rsa, string userName, string userEmail)
    {
        // Create a certificate request
        CertificateRequest request = new CertificateRequest(
            $"CN={userName}, Email={userEmail}",
            rsa,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);

        // Set the validity period of the certificate to 1 year
        DateTimeOffset notBefore = DateTimeOffset.Now;
        DateTimeOffset notAfter = notBefore.AddYears(1);

        // Add user information to the certificate
        request.CertificateExtensions.Add(
            new X509BasicConstraintsExtension(false, false, 0, false));

        request.CertificateExtensions.Add(
            new X509KeyUsageExtension(
                X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.NonRepudiation,
                false));

        // Create the self-signed certificate
        X509Certificate2 certificate = request.CreateSelfSigned(notBefore, notAfter);

        return certificate;
    }

    static void SaveToFile(string fileName, string content)
    {
        File.WriteAllText(fileName, content);
        Console.WriteLine($"Saved {fileName}");
    }

    static void SaveToFile(string fileName, byte[] content)
    {
        File.WriteAllBytes(fileName, content);
        Console.WriteLine($"Saved {fileName}");
    }
}
