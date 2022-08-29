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
        public TutorComment TutorComment { get; set; }
        public StudentComment StudentComment { get; set; }
        public User Student { get; set; }
        public Tutor Tutor { get; set; }
        public int RateTutor { get; set; }
        public int RateStudent { get; set; }
        public DateTime Startime { get; set; }
        public DateTime EndTime { get; set; }
        public SessionStatus Status { get; set; }
        public int RatingStudentCount { get; internal set; }
    }
}
