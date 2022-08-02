using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.DTOs
{
    public class AddTutorDTO : BaseRegisterDTO
    {
        public  string ShortBio { get; set; }
        public IEnumerable<AddTutorSubjectDTO> Subjects { get; set; }
        public IEnumerable<AddTutorAvailabilityDTO> Availability { get; set; }  
    }

    public class AddTutorSubjectDTO
    {
        public string Topic { get; set; }
        public string Description { get; set; }
    }

    public class AddTutorAvailabilityDTO
    {
        public string Day { get; set; }
    }
}
