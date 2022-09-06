using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialBuddy.Core.Enums
{
    public enum SessionStatus
    {
        requested = 0,
        accepted = 1,
        inprogress = 2,
        completed = 3,
        abandoned = 4,
        cancel = 5
       
    }
}
