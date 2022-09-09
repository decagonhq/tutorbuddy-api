using System;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core.Enums;

namespace TutorBuddy.Core.DTOs
{
	public class TutorSessionResponseDTO
	{
        
        public string? SessionId { get; set; }
        public string? Topic { get; set; }
        public string? Thumbnail { get; set; }
        public string? Student { get; set; }
		public string? StudentImage { get; set; }
        public DateTime Startime { get; set; }
        public DateTime EndTime { get; set; }
        public SessionStatus Status { get; set; }

    }
}

