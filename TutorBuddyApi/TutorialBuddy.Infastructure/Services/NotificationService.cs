using FindR.Integrations.NotificationProviders;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TutorialBuddy.Core.Enums;
using TutorialBuddy.Infastructure.NotificationProviders;

namespace FindR.Integrations
{
    public class NotificationService : INotificationService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly IConfiguration configuration;

        private readonly Dictionary<NotifyWith, INotificationProvider>
            _notificationProviders = new Dictionary<NotifyWith, INotificationProvider>();

        public NotificationService(
            ILogger<NotificationService> logger,
            IFluentEmail fluentEmail,
            IConfiguration configuration
            )

        {
            _fluentEmail = fluentEmail;
            this.configuration = configuration;

            _notificationProviders
                .Add(NotifyWith.Email, new SendGridEmailProvider());
            Logger = logger;
        }

        public ILogger<NotificationService> Logger { get; }

        public async Task<bool> SendAsync(NotifyWith target, NotificationContext payload)
        {
            //Inject DI instances, cos different providers might use different Instances.
            payload.FluentEmail = _fluentEmail;
            payload.Config = configuration;

            try
            {
                _ = !target.HasFlag(NotifyWith.Email) ||
                    await _notificationProviders[NotifyWith.Email].SendAsync(payload.Address, payload);
            }
            catch (Exception)
            {
                Logger.LogError($"notification Error: {target} => {payload.Header}");

                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }
    }
}