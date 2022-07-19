using FluentEmail.Core;
using Microsoft.Extensions.Configuration;

namespace TutorialBuddy.Infastructure.NotificationProviders
{
    public class NotificationContext
    {
        public string Address { get; set; } = string.Empty;
        public string Header { get; set; } = string.Empty;
        public string Payload { get; set; } = null!;

        public IFluentEmail? FluentEmail { get; set; } = null;
        public IConfiguration Config { get; set; } = null!;
    }
}