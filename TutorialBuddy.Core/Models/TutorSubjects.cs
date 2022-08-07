using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class TutorSubject: BaseEntity
    {
       
        public string SubjectID { get; set; }

        [ForeignKey("Tutor")]
        public string TutorID { get; set; }
        public IEnumerable<Session>? Sessions { get; set; }

    }
}
