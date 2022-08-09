using System;
namespace TutorBuddy.Core.Models
{
	public class Notification : BaseEntity
    {
        public string SenderId { get; set; }
        public string Message { get; set; }
        public bool Isread { get; set; }
        public User User { get; set; }
    }
}

