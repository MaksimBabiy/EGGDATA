namespace Server.Infrastructure.Test.Communications
{
    using System.Threading.Tasks;
    using Moq;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using Server.Infrastructure.Communications;
    using Xunit;

    public class SendGridFixure  
    {
        [Fact]
        public async Task SendEmailAsync_ReturnsSuccessResult()
        {
            const string emailFrom = "sender@gmail.com";
            const string emailTo = "getter@gmail.com";
            const string subject = "test subject";
            const string messageBody = "test body";
            const string guidKey = "899DE54D-6921-4827-A2A0-DFBA783AC899";

            Mock<SendGridClient> moq = new Mock<SendGridClient>(guidKey, null, null, null, null);
            SendGrid.Response result = await SendGridSender.SendEmailAsync(moq.Object, emailFrom, emailTo, subject, messageBody);

            Assert.True(!Equals(result.Body, null));
        }
    }
}
