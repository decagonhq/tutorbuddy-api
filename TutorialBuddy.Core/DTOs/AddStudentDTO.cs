using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.DTOs
{
    public class AddStudentDTO : BaseRegisterDTO
    {
        public IEnumerable<AddStudentSubjectDTO> AreaOfInterest { get; set; }
    }

    public class AddStudentSubjectDTO
    {
        public string Topic { get; set; }
        public string Description { get; set; }
    }
}
