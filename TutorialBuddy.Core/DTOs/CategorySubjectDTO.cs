using System;
namespace TutorBuddy.Core.DTOs
{
	public class CategorySubjectDTO
	{
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public IEnumerable<RecommendSubjectDTO>? Subject { get; set; }
    }
}

