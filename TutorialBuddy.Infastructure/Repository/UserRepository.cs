using Microsoft.EntityFrameworkCore;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly TutorBuddyContext _appDbContext;

        public UserRepository(TutorBuddyContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> GetAUserNotification(string userId)
        {
            var user = await _appDbContext.Users
                        .Include(x => x.Notifications)
                        .Where(x => x.Id == userId)
                        .FirstOrDefaultAsync();

            return user;
        }

        public Task<bool> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByRefreshToken(string token, string userId)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(x => x.RefreshToken == token && x.Id == userId);

            if (user == null)
                throw new ArgumentException($"User with id {userId} does not exist");

            return user;
        }

        

        public async Task<User> GetAUser(string Id, string role)
        {
            if(role == UserRole.Student.ToString())
            {
                var user = await _appDbContext.Users
                        .Where(x => x.Id == Id)
                        .FirstOrDefaultAsync();
                return user;
            }

            else
            {
                var user = await _appDbContext.Users
                        .Where(x => x.Id == Id)
                        .Include(x => x.Tutor)
                        .FirstOrDefaultAsync();
                return user;
            }
            
        }

        public async Task<User> GetAUserReminders(string userId)
        {
            
            var user = await _appDbContext.Users
                       .Include(x => x.Reminders)
                       .Where(x => x.Id == userId)
                       .FirstOrDefaultAsync();

            return user;
        }


        //private static DateTime GetDateExcludeWeekends(DateTime date, int index)
        //{
        //    var newDate = date.AddDays(-index);

        //    if (newDate.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        return newDate.AddDays(-2);
        //    }

        //    if (newDate.DayOfWeek == DayOfWeek.Saturday)
        //    {
        //        return newDate.AddDays(-1);
        //    }

        //    return DateTime.Now;
        //}
    }
}

