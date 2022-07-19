using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TutorialBuddy.Core.Models;

namespace TutorialBuddy.DataAccess
{
    public class TutorialBuddyContext : IdentityDbContext<User>
    {
        public TutorialBuddyContext(DbContextOptions<TutorialBuddyContext> options) : base(options)
        {

        }

    }
}