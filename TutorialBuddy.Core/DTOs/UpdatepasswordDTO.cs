using System;
namespace TutorBuddy.Core.DTOs
{
	public class UpdatePasswordDTO
	{
        public string Id { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        
    }
}

