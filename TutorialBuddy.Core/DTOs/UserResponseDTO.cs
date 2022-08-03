using System;
namespace TutorBuddy.Core.DTOs
{
	public class UserResponseDTO
	{
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }

        public IEnumerable<string> Subjects { get; set; }

    }
}

