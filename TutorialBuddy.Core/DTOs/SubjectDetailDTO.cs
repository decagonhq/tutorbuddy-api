using System;
namespace TutorBuddy.Core.DTOs
{
	public class SubjectDetailDTO
	{
        public string? Topic { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public int? NoOfCourses { get; set; }
        public string? BioNote { get; set; }
        public double? Price { get; set; }
        public string? UnitOfPrice { get; set; }
        public int? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IEnumerable<string>? TutorComments { get; set; }
    }
}

