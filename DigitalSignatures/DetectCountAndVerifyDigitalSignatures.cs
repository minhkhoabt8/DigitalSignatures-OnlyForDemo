using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignatures
{
    public class DetectCountAndVerifyDigitalSignatures
    {
        public DetectCountAndVerifyDigitalSignatures()
        {
        }


        /// <summary>
        /// This method used to check if a file has digital signatures
        /// </summary>
        /// <param name="dirtectory"></param>
        /// <param name="fileName"></param>
        public static void GetDetectCountAndVerifyDigitalSignatures(string filePath)
        {
            FileFormatInfo info = FileFormatUtil.DetectFileFormat(filePath);
            if (info.HasDigitalSignature)
            {
                Console.WriteLine(
                    $"Document {Path.GetFileName(filePath)} has digital signatures, " +
                    "they will be lost if you open/save this document with Aspose.Words.");
            }
        }
    }
}
