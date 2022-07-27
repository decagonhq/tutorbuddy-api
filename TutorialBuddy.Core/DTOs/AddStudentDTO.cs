using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.DTOs
{
    public class AddStudentDTO : BaseRegisterDTO
    {
        public IEnumerable<string> AreaOfInterest { get; set; }
    }
}
