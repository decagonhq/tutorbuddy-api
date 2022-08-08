using System;
namespace TutorBuddy.Core.DTOs
{
	public class RefreshTokenResponse
	{
        public string? NewAccessToken { get; set; }
        public string? NewRefreshToken { get; set; }
    }
}

