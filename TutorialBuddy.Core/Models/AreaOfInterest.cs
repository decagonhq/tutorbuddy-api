using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialBuddy.Core.Models
{
    public class AreaOfInterest: BaseEntity
    {
        public Subject Subject { get; set; }
        public User User { get; set; }
    }
}
