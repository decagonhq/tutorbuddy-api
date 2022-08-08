using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        public string? Bio { get; set; }

        public double? Price { get; set; }
        public string? UnitOfPrice { get; set; }

        public IEnumerable<SubjectDTO>? Subjects{ get; set; }
        public IEnumerable<AvailabilityDTO>? Avaliabilities { get; set; }
    }
}
