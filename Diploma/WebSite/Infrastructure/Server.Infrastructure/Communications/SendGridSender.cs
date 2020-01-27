namespace Server.Infrastructure.Communications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using Server.DataBaseCore.AppSettings;

    public class SendGridSender
    {
        public SendGridSender()
        {
            this.AppSettings = this.AppSettings;
        }

        protected AppSettings AppSettings { get; private set; }

        public static Task<Response> SendEmailAsync(SendGridClient sendGridClient, string emailFrom, string emailTo, string subject, string messageBody)
        {
            // create a client with key
            EmailAddress from = new EmailAddress(emailFrom);
            EmailAddress to = new EmailAddress(emailTo);

            List<Content> contents = new List<Content>()
            {
                new Content { Type = "text/plain", Value = "Here text for email if HTML is not working" },
                new Content { Type = "text/html", Value = messageBody }
            };

            // create a Mail object to send
            SendGridMessage message = new SendGridMessage() { From = from, Subject = subject, Contents = contents };

            message.AddTo(to);

            // disable SendGrid email tracking
            message.TrackingSettings = new TrackingSettings { ClickTracking = new ClickTracking { Enable = false } };

            return sendGridClient.SendEmailAsync(message);
        }
    }
}
