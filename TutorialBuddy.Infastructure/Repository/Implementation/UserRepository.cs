using System;
using TutorBuddy.Infrastructure.Repository.Interface;
using TutorialBuddy.Core.Models;
using TutorialBuddy.Infastructure;

namespace TutorBuddy.Infrastructure.Repository.Implementation
{
	public class UserRepository :  GenericRepository<User>, IUserRepository
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
    }
}

