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
        public IEnumerable<string> Subjects { get; set; }
        private IEnumerable<string> Availability { get; set; }  
    }
}
