using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialBuddy.Core
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
    }
}
