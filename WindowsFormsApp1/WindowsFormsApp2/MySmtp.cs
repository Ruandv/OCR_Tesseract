using BusinessLayer;
using System;
using System.IO;
using System.Net.Mail;

namespace WindowsFormsApp2
{
    public class MySmtp : ISmtpInfo
    {
        //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

        public void Send(byte[] protectedDocument)
        {

            try
            {
                MailMessage mail = new MailMessage("ruan.devilliers@absolutesys.com", "ruandv@gmail.com");
                SmtpClient client = new SmtpClient();

                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.sendgrid.net";
                client.Credentials = new System.Net.NetworkCredential("apikey", "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");
                mail.Attachments.Add(new Attachment(protectedDocument.ToStream(), "payslip"));
                mail.Subject = Guid.NewGuid().ToString();
                mail.Body = Guid.NewGuid().ToString() + "this is my test email body" + Guid.NewGuid().ToString();
                //client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
