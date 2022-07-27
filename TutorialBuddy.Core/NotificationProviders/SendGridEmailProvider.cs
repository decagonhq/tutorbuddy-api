using SendGrid;
using SendGrid.Helpers.Mail;
using TutorialBuddy.Infastructure.NotificationProviders;

namespace FindR.Integrations.NotificationProviders
{
    public class SendGridEmailProvider : INotificationProvider
    {
        public async Task<bool> SendAsync(string recipientAddress, NotificationContext nctx)
        {
            var apikey = nctx.Config["FluentEmail:SendGridPkey"];
            var client = new SendGridClient(apikey);

            var msg = new SendGridMessage
            {
                From = new EmailAddress("tbuddy.support@gmail.com", "Tutorial Buddy"),
                Subject = nctx.Header,
                PlainTextContent = nctx.Payload.ToString(),
            };
            msg.AddTo(new EmailAddress(recipientAddress!, "Tbuddy User"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}