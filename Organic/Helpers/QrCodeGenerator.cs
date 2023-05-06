//using QRCoder;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.PixelFormats;
//using SixLabors.ImageSharp.Processing;

//namespace Organic.Helpers
//{
//    public class QrCodeGenerator
//    {
//        public static byte[] GenerateByteArray(string url)
//        {
//            var image = GenerateImage(url);
//            return ImageToByte(image);
//        }

//        public static Image<Rgba32> GenerateImage(string url)
//        {
//            QRCodeGenerator qrGenerator = new QRCodeGenerator();
//            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
//            var qrCode = new QRCode(qrCodeData);
//            var qrCodeImage = qrCode.GetGraphic(10);

//            using var stream = new MemoryStream(qrCodeImage);
//            var image = Image.Load<Rgba32>(stream);

//            return image;
//        }

//        private static byte[] ImageToByte(Image<Rgba32> img)
//        {
//            using var stream = new MemoryStream();
//            img.Save(stream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
//            return stream.ToArray();
//        }
//    }
//}
