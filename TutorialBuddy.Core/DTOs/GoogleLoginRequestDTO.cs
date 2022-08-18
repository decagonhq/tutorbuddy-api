using System;
namespace TutorBuddy.Core.DTOs
{
	public class GoogleLoginRequestDTO
	{
        public string? Provider { get; set; }
        public string? IdToken { get; set; }
    }
}

