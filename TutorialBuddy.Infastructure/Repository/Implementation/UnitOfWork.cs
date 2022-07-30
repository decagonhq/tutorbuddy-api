using System;
using TutorBuddy.Infrastructure.Repository.Interface;
using TutorialBuddy.Infastructure;

namespace TutorBuddy.Infrastructure.Repository.Implementation
{
	public class UnitOfWork : IUnitOfWork
    {
        private readonly TutorBuddyContext _appDbContext;
        private IUserRepository _userRepository;
        public UnitOfWork(TutorBuddyContext appDbContext)
		{
            _appDbContext = appDbContext;
		}

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_appDbContext);
    }
}

