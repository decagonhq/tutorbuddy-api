using TutorialBuddy.Infastructure.NotificationProviders;

namespace FindR.Integrations
{
    public interface INotificationProvider
    {
        Task<bool> SendAsync(string recipientAddress, NotificationContext payload);
    }
}