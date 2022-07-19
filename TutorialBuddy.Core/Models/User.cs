using Microsoft.AspNetCore.Identity;

namespace TutorialBuddy.Core.Models
{
    public class User : IdentityUser
    {

        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? RefreshToken { get; set; }

    }
}
