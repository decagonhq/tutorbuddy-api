using System;
using Microsoft.AspNetCore.Identity;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.DTOs
{
	public class GetRegisterResponseDTO
	{
        public IEnumerable<string>? Roles { get; set; }
        public IEnumerable<AvailabilityDTO>? Avaliabilities { get; set; }
        public IEnumerable<SubjectDTO>? Subjects { get; set; }
    }
}

