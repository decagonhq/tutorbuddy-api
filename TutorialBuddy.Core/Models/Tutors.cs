using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class Tutor
    {
        [Key]
        public string UserId { get; set; }
        public string? BioNote { get; set; }
        public double? Price { get; set; }
        public string? UnitOfPrice { get; set; }
        public User User { get; set; }
        public IEnumerable<Availability> Availability { get; set; }
        public IEnumerable<TutorSubjects> TutorSubjects { get; set; }

    }
}
