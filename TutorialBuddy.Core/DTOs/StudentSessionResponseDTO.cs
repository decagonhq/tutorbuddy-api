using System;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core.Enums;

namespace TutorBuddy.Core.DTOs
{
	public class StudentSessionResponseDTO
	{
        //public IEnumerable<StudentSessionDTO>? Sessions { get; set; }
        //public SubjectDTO? Subject { get; set; }
        public string? SessionId { get; set; }
        public string? Topic { get; set; }
        public string? Thumbnail { get; set; }
        public string? Tutor { get; set; }
        public string? TutorImage { get; set; }
        public DateTime Startime { get; set; }
        public DateTime EndTime { get; set; }
        public SessionStatus Status { get; set; }
        
        
    }
}

