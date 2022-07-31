using System;
using System.ComponentModel.DataAnnotations;

namespace TutorBuddy.Core.DTOs
{
	public class UpdateUserDTO
	{
        [Required]
        public string FullName{ get; set; }

        [Required]
        public string Role { get; set; }
        public IEnumerable<string> Subjects { get; set; }

        
    }
}

