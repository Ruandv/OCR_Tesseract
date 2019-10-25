using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using BusinessLayer;
using Attachment = System.Net.Mail.Attachment;

namespace SteadyPayout
{
    public class Office365 : ISmtpInfo
    {
        public void Send(string toEmailAddress, string subject, string body, byte[] protectedDocument)
        {
            SmtpClient client = new SmtpClient();
            client.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = ConfigurationManager.AppSettings["Host"];
            client.Credentials = new System.Net.NetworkCredential("ruan.devilliers@absolutesys.com", ConfigurationManager.AppSettings["ApiKey"]);

            var msg = new MailMessage(ConfigurationManager.AppSettings["FromEmailAddress"], toEmailAddress, subject, body);

            Stream stream = new MemoryStream(protectedDocument);

            msg.Attachments.Add(new Attachment(stream, "payslip.pdf"));
            client.Send(msg);

        }
    }
}