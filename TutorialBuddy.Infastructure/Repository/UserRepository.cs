using Microsoft.EntityFrameworkCore;
using TutorBuddy.Core.Interface;
using TutorialBuddy.Core.Models;
using TutorialBuddy.Infastructure.DataAccess;

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
    }
}

