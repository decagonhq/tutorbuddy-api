using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialBuddy.Core.Models
{
    public class Tutor
        : BaseEntity
    {
        public string BioNote { get; set; }
        public User User { get; set; }
    }
}
