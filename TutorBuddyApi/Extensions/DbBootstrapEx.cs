using Microsoft.AspNetCore.Identity;
using TutorBuddy.Core.Enums;

namespace TutorBuddyApi
{
    public class DbBootstrapEx
    {
        public DbBootstrapEx(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string>() { UserRole.Tutor.ToString(), UserRole.Student.ToString() };

            if (!roleManager.Roles.Any())
            {
                roles.ForEach(role =>
                {
                    var res = roleManager.CreateAsync(new IdentityRole(role)).Result;
                });
            }
        }
    }
}