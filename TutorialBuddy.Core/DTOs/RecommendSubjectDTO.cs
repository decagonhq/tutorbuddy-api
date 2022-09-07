using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.DTOs
{
	public class RecommendSubjectDTO
	{
		public SubjectRecommedDTO? Subject { get; set; }
        public string? TutorSubjectId { get; set; }
        public string? Tutor { get; set; }
        public int Rate { get; set; }
        public int UserCount { get; set; }
    }
}

