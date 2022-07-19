using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialBuddy.Core.Models
{
    public class RateTutor: BaseEntity
    {
        public int Rate { get; set; }
        public Session Session { get; set; }
    }
}
