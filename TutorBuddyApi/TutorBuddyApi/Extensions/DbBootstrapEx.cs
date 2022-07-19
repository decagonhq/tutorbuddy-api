using Microsoft.AspNetCore.Identity;

namespace FindRApi.Extensions
{
    public class DbBootstrapEx
    {
        public DbBootstrapEx(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string>() { "admin", "user", "staff", "provider" };

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