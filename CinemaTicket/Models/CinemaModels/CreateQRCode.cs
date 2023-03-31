using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace WebBanHangOnline.Common
{
    public class CreateQRCode
    {
        public Bitmap picMomo;
        public void CreateQRCodeMomo(string txtSDT, string txtHoTen, string txtMail, string txtTien)
        {
            Image MoMo_Logo = Image.FromFile("C:\\Users\\ADMIN\\Pictures\\253542737_922747162003884_8651428017669891785_n.jpg");
            //Image MoMo_Logo = Image.FromFile("~/Content/image.jpg");
            var qrcode_text = $"2|99|{txtSDT.Trim()}|{txtHoTen.Trim()}|{txtMail.Trim()}|0|0|{txtTien.Trim()}";
            Regex re = new Regex("^0[0-9]{8}");
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions() { Width = 250, Height = 250, Margin = 0, PureBarcode = false };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            Bitmap bitmap = barcodeWriter.Write(qrcode_text);
            Bitmap logo = resizeImage(MoMo_Logo, 64, 64) as Bitmap;
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(logo, new Point((bitmap.Width - logo.Width) / 2, (bitmap.Height - logo.Height) / 2));
            picMomo = bitmap;
        }

        public Image resizeImage(Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }
        public byte[] turnImageToByteArray(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }
    }
}