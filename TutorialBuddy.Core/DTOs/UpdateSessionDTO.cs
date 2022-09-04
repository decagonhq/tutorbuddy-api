using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialBuddy.Core.Enums;

namespace TutorBuddy.Core.DTOs
{
    public class UpdateSessionDTO
    {
        public string Id { get; set; }
        public SessionStatus Status { get;  set; }
    }
}
