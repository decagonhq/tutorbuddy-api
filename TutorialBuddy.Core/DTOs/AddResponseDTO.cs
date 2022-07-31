using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBuddy.Core.DTOs
{
    public class AddResponseDTO
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
