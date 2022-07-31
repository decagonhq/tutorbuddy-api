using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class Tutor
        : BaseEntity
    {
        public string? BioNote { get; set; }
        public double? Price { get; set; }
        public string? UnitOfPrice { get; set; }
        public User User { get; set; }
    }
}
