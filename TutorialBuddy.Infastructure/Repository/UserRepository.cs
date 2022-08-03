using Microsoft.EntityFrameworkCore;
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

        public Task<User> GetAUserNotification(string userId)
        {
            throw new NotImplementedException();
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

        public async Task AddUserAreaOfInterestA(User user, IEnumerable<Subject> subjects)
        {
            if (subjects.Any())
            {
                var userAreaOfInterest = new AreaOfInterest()
                {
                    User = user,
                    Subjects = subjects
                };
                await _appDbContext.AreaOfInterests.AddAsync(userAreaOfInterest);
            }
        }

        public async Task<User> GetAUser(string Id, string role)
        {
            if(role == "Student")
            {
                var user = await _appDbContext.Users
                        .Where(x => x.Id == Id)
                        .Include(x => x.AreaOfInterests)
                            .ThenInclude(x => x.Subjects)
                        .FirstOrDefaultAsync();
                return user;
            }

            else
            {
                var user = await _appDbContext.Users
                        .Where(x => x.Id == Id)
                        .Include(x => x.Tutors)
                        .FirstOrDefaultAsync();
                return user;
            }
            
        }
    }
}

