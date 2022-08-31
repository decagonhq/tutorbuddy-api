using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class TutorComment: BaseEntity
    {
        public string? Comment { get; set; }
        public string SessionId { get; set; }
        public Session Sessiom { get; set; }
    }
}
