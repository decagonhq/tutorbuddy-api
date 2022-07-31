namespace TutorBuddy.Core.Interface
{
	public interface IUnitOfWork : IDisposable
	{
        IUserRepository UserRepository { get; }
        Task Save();
    }
}

