using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class Subject: BaseEntity
    {
        public Category Category { get; set; }
        public string Topic { get; set; }
        public string? Thumbnail { get; set; }
        public string? Description { get; set; }
        public IEnumerable<TutorSubjects> TutorSubjects { get; set; }

    }
}
