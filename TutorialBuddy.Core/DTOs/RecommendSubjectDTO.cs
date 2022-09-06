using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.DTOs
{
	public class RecommendSubjectDTO
	{
		public Subject? Subject { get; set; }
		public string? Tutor { get; set; }
        public int Rate { get; set; }
    }
}

