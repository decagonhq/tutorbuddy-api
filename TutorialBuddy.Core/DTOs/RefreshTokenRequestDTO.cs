using System;
namespace TutorBuddy.Core.DTOs
{
	public class RefreshTokenRequestDTO
	{
        public string? UserId { get; set; }
        public string? RefreshToken { get; set; }
    }
}

