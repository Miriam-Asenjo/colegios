using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;


namespace Colegios.ApplicationServices
{
    public static class MailManager
    {
        private static readonly string SmtpServer = ConfigurationManager.AppSettings["smtpServer"];
        private static readonly int SmtpPort = Int32.Parse(ConfigurationManager.AppSettings["smtpPort"]);
        private static readonly string SmtpAccount = ConfigurationManager.AppSettings["smtpAccount"];
        private static readonly string SmtpPassword = ConfigurationManager.AppSettings["smtpPassword"];

        public static void SendMail(string toList, string from, string ccList, string subject, string body)
        {
            var message = new MailMessage();
            var smtpClient = new SmtpClient();
            string msg = string.Empty;

            var fromAddress = new MailAddress(from);
            message.From = fromAddress;
            message.To.Add(toList);
            if (ccList != null && ccList != string.Empty)
                message.CC.Add(ccList);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            smtpClient.Host = SmtpServer;
            smtpClient.Port = SmtpPort;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(from ?? SmtpAccount, SmtpPassword);

            smtpClient.Send(message);


        }

    }
}
