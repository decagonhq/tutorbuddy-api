using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class AreaOfInterest: BaseEntity
    {
        public IEnumerable<Subject> Subjects { get; set; }
        public User User { get; set; }
    }
}
