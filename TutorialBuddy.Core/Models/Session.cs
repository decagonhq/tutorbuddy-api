using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialBuddy.Core.Enums;

namespace TutorialBuddy.Core.Models
{
    public class Session: BaseEntity
    {
        public Subject Subject { get; set; }
        public Tutor Tutor { get; set; }
        public User Student { get; set; }
        public DateTime Startime { get; set; }
        public DateTime EndTime { get; set; }
        public SessionStatus Status { get; set; }
    }
}
