using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class TutorSubject: BaseEntity
    {
        public string SubjectID { get; set; }
        public string TutorID { get; set; }
        public IEnumerable<Session>? Sessions { get; set; }

    }
}
