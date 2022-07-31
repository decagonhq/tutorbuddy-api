using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
