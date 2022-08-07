using TutorialBuddy.Core.Enums;
using TutorialBuddy.Infastructure.NotificationProviders;

public interface INotificationService
{
    Task<bool> SendAsync(NotifyWith target, NotificationContext payload);
}