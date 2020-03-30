using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImgToTxtApp
{
    class Program
    {
        private static string imgPath = @"c:\DEMOS\image.png";

        static void Main(string[] args)
        {
            using (Vintasoft.Imaging.Ocr.Tesseract.TesseractOcr tesseractOcr = new Vintasoft.Imaging.Ocr.Tesseract.TesseractOcr())
            {
                // Set Languages
                Vintasoft.Imaging.Ocr.OcrLanguage language1 = Vintasoft.Imaging.Ocr.OcrLanguage.English;
                Vintasoft.Imaging.Ocr.OcrLanguage language2 = Vintasoft.Imaging.Ocr.OcrLanguage.Turkish;
                Vintasoft.Imaging.Ocr.OcrLanguage[] languages = new Vintasoft.Imaging.Ocr.OcrLanguage[] { language1, language2};

                // Create Settings
                Vintasoft.Imaging.Ocr.Tesseract.TesseractOcrSettings settings = new Vintasoft.Imaging.Ocr.Tesseract.TesseractOcrSettings(languages);

                // Initialize Ocr Engine
                tesseractOcr.Init(settings);

                using (Vintasoft.Imaging.VintasoftImage image = new Vintasoft.Imaging.VintasoftImage(imgPath))
                {
                    tesseractOcr.SetImage(image);

                    //recognize text in the image
                    Vintasoft.Imaging.Ocr.Results.OcrPage ocrResult = tesseractOcr.Recognize();

                    // get recognized text 
                    string recognizedText = ocrResult.GetText();
                    recognizedText = recognizedText.Replace("VintaSoftImaging.NET DEMO", string.Empty);
                    string[] recognizedWords = recognizedText.Split('\n');

                    // save text to txt file
                    string txtFilePath = Path.Combine( Path.GetDirectoryName(imgPath) + @"\" +Path.GetFileNameWithoutExtension(imgPath) + ".txt");
                    //File.WriteAllText(txtFilePath, recognizedText);
                    File.WriteAllLines(txtFilePath, recognizedWords);
                    

                    // clear image
                    tesseractOcr.ClearImage();
                }

                tesseractOcr.Shutdown();
            }
        }
    }
}
