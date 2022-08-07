using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Seeder
{
    public class Seeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly TutorBuddyContext _dbContext;
        public Seeder(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, TutorBuddyContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        public async Task Seed()
        {
            await _dbContext.Database.EnsureCreatedAsync();

            await CreateUserRolesAsync(_roleManager);
            await SeedCategory(_dbContext);     // seed categories and its subject
            await SeedAvalaibility(_dbContext);
            await SeedUser(_userManager, _dbContext);
            await SeedTutorSubject(_dbContext);

        }

        private static async Task CreateUserRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var userRoles = new List<IdentityRole>
            {
                new IdentityRole(UserRole.Admin.ToString()),
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

        private static async Task SeedUser(UserManager<User> userManager, TutorBuddyContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var users = SeederHelper<User>.GetData("Users.json");
                int count = 1;
                foreach (var user in users)
                {
                    if (count == 1)
                    {
                        user.EmailConfirmed = true;
                        var result = await userManager.CreateAsync(user, "Jan1@125");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
                        }
                    }
                    else if (count < 6)
                    {
                        user.EmailConfirmed = true;
                        await userManager.CreateAsync(user, "Jan2@124");
                        await userManager.AddToRoleAsync(user, UserRole.Tutor.ToString());
                    }
                    else
                    {
                        user.EmailConfirmed = true;
                        await userManager.CreateAsync(user, "Jan3@123");
                        await userManager.AddToRoleAsync(user, UserRole.Student.ToString());
                    }
                    count++;
                }
            }
        }

        private static async Task SeedCategory(TutorBuddyContext dbContext)
        {
            if (!dbContext.Categories.Any())
            {
                var categories = SeederHelper<Category>.GetData("Category.json");

                await dbContext.Categories.AddRangeAsync(categories);
            }
        }

        private static async Task SeedAvalaibility(TutorBuddyContext dbContext)
        {
            if (!dbContext.Availabilities.Any())
            {
                var avaliability = SeederHelper<Availability>.GetData("Avaliability.json");

                await dbContext.Availabilities.AddRangeAsync(avaliability);
            }   
        }

        private static async Task SeedTutorSubject(TutorBuddyContext dbContext)
        {
            if (!dbContext.TutorSubjects.Any())
            {
                var tutorSubjects = SeederHelper<TutorSubjects>.GetData("TutorSubject.json");

                await dbContext.TutorSubjects.AddRangeAsync(tutorSubjects);
            }
        }
    }
}