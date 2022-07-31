using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.Models
{
    public class Subject: BaseEntity
    {
        public string Topic { get; set; }
        public string Description { get; set; }

    }
}
