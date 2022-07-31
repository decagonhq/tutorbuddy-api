using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class TutorSubjects: BaseEntity
    {
        public IEnumerable<Subject> Subjects { get; set; }
        public Tutor Tutor { get; set; }

    }
}
