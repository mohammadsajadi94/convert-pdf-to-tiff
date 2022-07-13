using EvoPdf.PdfToImage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.License;
using Spire.Pdf;

namespace convert_pdf_to_tiff
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileStream fileMht = new FileStream("enter path file here", FileMode.Open);
            byte[] bfile = new byte[fileMht.Length];
            fileMht.Read(bfile, 0, Convert.ToInt32(bfile.Length));
            byte[] PdfFile = ConvertPdfToTiff(bfile);
            fileMht.Close();
        }
        public static byte[] ConvertPdfToTiff(byte[] byteArray)
        {
            MemoryStream st = new MemoryStream(byteArray);
            Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
            pdf.LoadFromStream(st);
            string tempPath = System.IO.Path.GetTempPath() + DateTime.Now.Year.ToString() + "\\";
            if (System.IO.Directory.Exists(tempPath))
            {
                DirectoryInfo dir = new System.IO.DirectoryInfo(tempPath);
                dir.Delete(true);
            }
            System.IO.Directory.CreateDirectory(tempPath);
            string fileName = tempPath + "output.tiff";

            //////////////////////
            PdfToImageConverter pdfToImageConverter = new PdfToImageConverter();

            pdfToImageConverter.LicenseKey = "0F5PX0pGX09fSVFPX0xOUU5NUUZGRkZfTw==";

            // set the color space of the resulted images
            pdfToImageConverter.ColorSpace = PdfPageImageColorSpace.RGB;

            // set the resolution of the resulted images
            pdfToImageConverter.Resolution = 300;
            //pdfToImageConverter.PageConvertedEvent += PdfToImageConverter_PageConvertedEvent;
            if (pdf.Pages.Count > 1)
            {
                // save the PDF as a multipage TIFF image having a frame for each converted PDF page
                pdfToImageConverter.ConvertPdfToTiffFile(byteArray, 1, pdf.Pages.Count, fileName);
            }
            else
            {
                pdfToImageConverter.ConvertPdfToTiffFile(byteArray, 1, pdf.Pages.Count, fileName);
            }
            byte[] byte_file = File.ReadAllBytes(fileName);
            File.Delete(fileName);
            return byte_file;
        }
    }
}
