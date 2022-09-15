using System;
namespace TutorBuddy.Core.DTOs
{
	public class ReminderDTO
	{
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Note { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

