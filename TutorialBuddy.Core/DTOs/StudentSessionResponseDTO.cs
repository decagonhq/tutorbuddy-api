using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.DTOs
{
	public class StudentSessionResponseDTO
	{
        public IEnumerable<SessionDTO>? Sessions { get; set; }
        public SubjectDTO? Subject { get; set; }
        public string? Tutor { get; set; }
        
    }
}

