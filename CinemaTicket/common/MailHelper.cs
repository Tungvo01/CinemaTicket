using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace CinemaTicket.common
{
    public class MailHelper
    {
        public void SendMail(string toEmailAddress, string subject, string content)
        {

            // Để Gmail cho phép SmtpClient kết nối đến server SMTP của nó với xác thực 
            //là tài khoản gmail của bạn, bạn cần thiết lập tài khoản email của bạn như sau:
            //Vào địa chỉ https://myaccount.google.com/security  Ở menu trái chọn mục Bảo mật, sau đó tại mục Quyền truy cập 
            //của ứng dụng kém an toàn phải ở chế độ bật
            //  Đồng thời tài khoản Gmail cũng cần bật IMAP
            //Truy cập địa chỉ https://mail.google.com/mail/#settings/fwdandpop

            var fromEmailAddress = "sendcodetw@gmail.com";
            var fromEmailDisplayName = "Vé xem phim";
            var fromEmailPassword = "cfhzuhybjjrdbwzq";
            var smtpHost = "smtp.gmail.com";
            var smtpPort = "587";

           
            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
           
          
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            //client.EnableSsl = enabledSsl;
            client.Port = Convert.ToInt32(smtpPort);
            client.Send(message);
        }
    }
}