using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.DTOs
{
	public class TutorSessionResponseDTO
	{
		public IEnumerable<TutorSessionDTO>? Sessions { get; set; }
		public SubjectDTO? Subject { get; set; }
		public string? Student { get; set; }
		public string? StudentImage { get; set; }
		
	}
}

