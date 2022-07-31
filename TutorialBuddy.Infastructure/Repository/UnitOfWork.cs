using TutorBuddy.Core.Interface;
using TutorialBuddy.Infastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
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

        public void Dispose()
        {
            _appDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}

