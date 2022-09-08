using System;
namespace TutorBuddy.Core.DTOs
{
	public class AddReminderRequestDTO
	{
        public string? Title { get; set; }
        public string? Note { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Provider { get; set; }
    }
}

