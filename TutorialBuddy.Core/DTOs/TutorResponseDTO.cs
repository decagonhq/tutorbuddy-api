using System;
namespace TutorBuddy.Core.DTOs
{
	public class TutorResponseDTO
	{
		public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public string? BioNote { get; set; }
        public IEnumerable<string>? Subject { get; set; }
        public IEnumerable<string>? Avaliabilities { get; set; }

    }
}

