using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebBanHangOnline.Common;

namespace WebBanHangOnline.Controllers
{
    public class MomoController : Controller
    {
        // GET: Momo
        public ActionResult Index()
        {
            var qrcode = new CreateQRCode();
            qrcode.CreateQRCodeMomo("0392958914", "aaa", "a2123", "1000");
            byte[] imgBytes = qrcode.turnImageToByteArray(qrcode.picMomo);
            string imgString = Convert.ToBase64String(imgBytes);
            ViewBag.Bitmap = String.Format("img src=data:image/Bmp;base64,{0}", imgString);

            return View();
        }
    }
}