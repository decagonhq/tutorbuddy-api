using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using TutorialBuddy.Infastructure.NotificationProviders;

namespace FindR.Integrations.NotificationProviders
{
    public class SendGridEmailProvider : INotificationProvider
    {
        public async Task<bool> SendAsync(NotificationContext nctx)
        {
            var apikey = nctx.Config.GetValue<string>("FluentEmail:SendGridPKey"); ;
            var client = new SendGridClient(apikey);

            var msg = new SendGridMessage
            {
                From = new EmailAddress("emma4lil@gmail.com", "Tutor Buddy"),
                Subject = nctx.Header,
                HtmlContent = nctx.Payload.ToString(),
            };
            msg.AddTo(new EmailAddress(nctx.Address!, "Tbuddy User"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}