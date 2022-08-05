using SendGrid;
using SendGrid.Helpers.Mail;
using TutorialBuddy.Infastructure.NotificationProviders;

namespace FindR.Integrations.NotificationProviders
{
    public class SendGridEmailProvider : INotificationProvider
    {
        public async Task<bool> SendAsync(NotificationContext nctx)
        {
            //var apikey = nctx.Config["FluentEmail:SendGridPkey"];
            var client = new SendGridClient("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");

            var msg = new SendGridMessage
            {
                From = new EmailAddress("emma4lil@gmail.com", "Tutorial Buddy"),
                Subject = nctx.Header,
                HtmlContent = nctx.Payload.ToString(),
            };
            msg.AddTo(new EmailAddress(nctx.Address!, "Tbuddy User"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}