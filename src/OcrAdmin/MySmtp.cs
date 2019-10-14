using BusinessLayer;
using System;
using System.Configuration;
using System.Net.Mail;

namespace WindowsFormsApp2
{
    public class MySmtp : ISmtpInfo
    {
        SmtpClient client = new SmtpClient();
        public MySmtp()
        {
            client.Port = int.Parse(ConfigurationManager.AppSettings.Get("SmtpPort"));
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = ConfigurationManager.AppSettings.Get("Host");
            client.Credentials = new System.Net.NetworkCredential("apikey", ConfigurationManager.AppSettings.Get("ApiKey"));
        }

        public void Send(string toEmailAddress, string subject, string body, byte[] protectedDocument)
        {
            try
            {
                MailMessage mail = new MailMessage("System@absolutesys.com", toEmailAddress);
                mail.Attachments.Add(new Attachment(protectedDocument.ToStream(), Guid.NewGuid().ToString()));
                mail.Subject = subject;
                mail.Body = body;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
