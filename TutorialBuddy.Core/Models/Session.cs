using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialBuddy.Core.Enums;

namespace TutorBuddy.Core.Models
{
    public class Session: BaseEntity
    {
        public TutorSubject TutorSubject { get; set; }
        public User Student { get; set; }
        public DateTime Startime { get; set; }
        public DateTime EndTime { get; set; }
        public SessionStatus Status { get; set; }
        public IEnumerable<TutorComment> TutorComments { get; set; }
        public IEnumerable<RateTutor> RateTutors { get; set; }
        public IEnumerable<StudentComment> StudentComments { get; set; }
        public IEnumerable<RateStudent> RateStudents { get; set; }
    }
}
