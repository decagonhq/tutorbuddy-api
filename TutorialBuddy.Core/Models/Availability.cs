using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialBuddy.Core.Models
{
    public class Availability: BaseEntity
    {
        public DateOnly Day { get; set; }
        public Tutor Tutor { get; set; }
    }
}
