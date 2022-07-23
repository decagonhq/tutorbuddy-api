namespace TutorialBuddy.Core.Enums
{
    [Flags]
    public enum NotifyWith
    {
        Email = 0,
        SMS = 1,
        SignalR = 2,
        OneSignal = 3,
    }
}