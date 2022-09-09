using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.DTOs
{
	public class RecommendSubjectDTO
	{
        public string? ID { get; set; }
        public string? Subject { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public string? TutorSubjectId { get; set; }
        public string? Tutor { get; set; }
        public int Rate { get; set; }
        public int UserCount { get; set; }
    }
}

