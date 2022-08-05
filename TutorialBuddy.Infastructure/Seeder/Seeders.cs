using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Seeder
{
    public class Seeders
    {
  

        public class Seeder
        {
            public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, TutorBuddyContext dbContext)
            {
                await dbContext.Database.EnsureCreatedAsync();

                await CreateUserRolesAsync(roleManager);
               // await SeedUser(userManager, dbContext);
                
            }

            private static async Task CreateUserRolesAsync(RoleManager<IdentityRole> roleManager)
            {
                var userRoles = new List<IdentityRole>
            {
                new IdentityRole(UserRole.Tutor.ToString()),
                new IdentityRole(UserRole.Student.ToString()),
               
            };
                if (!roleManager.Roles.Any())
                {
                    foreach (IdentityRole role in userRoles)
                    {
                        var result = await roleManager.CreateAsync(role);
                    }
                }
            }

            //private static async Task SeedUser(UserManager<User> userManager, TutorBuddyContext dbContext)
            //{
            //    if (!dbContext.Users.Any())
            //    {
            //        var users = SeederHelper<User>.GetData("User.json");
            //        int count = 1;
            //        foreach (var user in users)
            //        {
            //            if(count == 1)
            //            {
            //                user.EmailConfirmed = true;
            //                var result = await userManager.CreateAsync(user, "Ja@125");
            //                if (result.Succeeded)
            //                {
            //                    await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            //                }
            //            }
            //            else if(count < 6)
            //            {
            //                user.EmailConfirmed = true;
            //                await userManager.CreateAsync(user, "Ja@124");
            //                await userManager.AddToRoleAsync(user, UserRoles.Dealer.ToString());
            //            }
            //            else
            //            {
            //                user.EmailConfirmed = true;
            //                await userManager.CreateAsync(user, "Ja@123");
            //                await userManager.AddToRoleAsync(user, UserRoles.Customer.ToString());
            //            }
            //            count++;
            //        }
            //    }
            //}

          
        }
    }
}