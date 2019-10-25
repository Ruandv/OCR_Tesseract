using System;
using System.Configuration;
using BusinessLayer;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SteadyPayout
{
    public class SendGrid : ISmtpInfo
    {
        private readonly SendGridClient client;
        private readonly EmailAddress fromAddress;


        public SendGrid()
        {
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            client = new SendGridClient(apiKey);

            fromAddress = new EmailAddress(ConfigurationManager.AppSettings["FromEmailAddress"], "Payslip");
            //var to = new EmailAddress(emailAddress);

        }

        public void Send(string toEmailAddress, string subject, string body, byte[] protectedDocument)
        {
            var msg = MailHelper.CreateSingleEmail(fromAddress, new EmailAddress(toEmailAddress), subject, "", body);
            var file = Convert.ToBase64String(protectedDocument);
            msg.AddAttachment("payslip.pdf", file);
            client.SendEmailAsync(msg);

        }
    }
}
