namespace TutorBuddy.Core.Interface
{
	public interface IUnitOfWork : IDisposable
	{
        IUserRepository UserRepository { get; }
        ITutorRepository TutorRepository { get; }
        Task<bool> Save();
    }
}

