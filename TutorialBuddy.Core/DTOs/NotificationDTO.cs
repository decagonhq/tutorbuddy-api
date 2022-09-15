using System;
namespace TutorBuddy.Core.DTOs
{
	public class NotificationDTO
	{
		public string? Id { get; set; }

        public string? SenderName { get; set; }

        public string? SenderImage { get; set; }

        public string? Message { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
}

