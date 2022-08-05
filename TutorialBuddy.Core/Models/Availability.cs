using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class Availability: BaseEntity
    {
        public string Day { get; set; }
        public IEnumerable<Tutor> Tutor { get; set; }

    }
}
