using System;
using System.ComponentModel.DataAnnotations;

namespace TutorBuddy.Core.DTOs
{
	public class UpdateUserDTO
	{
        [Required]
        public string FirstName{ get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        

        
    }
}

