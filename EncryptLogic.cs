using System;
using System.Diagnostics;
using System.IO;

namespace PdfApi
{
    static class EncryptLogic
    {
        public static Stream EncryptPDFWithQPDF(Stream inputPdf, string password)
        {
            string tempPdfPath = Path.GetTempFileName();
            using (var tempInputPdfStream = new FileStream(tempPdfPath, FileMode.Create, FileAccess.Write))
            {
                inputPdf.CopyTo(tempInputPdfStream);
            }
            inputPdf.Close();

            var processInfo = new ProcessStartInfo
            {
                FileName = "qpdf",
                Arguments = $"--encrypt {password} {password} 256 -- --replace-input {tempPdfPath}",
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try 
            {
                using (var process = new Process { StartInfo = processInfo })
                {
                    process.Start();

                    var errorOutput = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode != 0) throw new Exception($"Error encrypting PDF: {errorOutput}");
                }

                var outputPdfStream = new MemoryStream(File.ReadAllBytes(tempPdfPath));
                outputPdfStream.Position = 0;
                return outputPdfStream;
            }
            finally 
            {
                if (File.Exists(tempPdfPath)) File.Delete(tempPdfPath);
            }
        }
    }
}
