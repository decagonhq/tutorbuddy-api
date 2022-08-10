namespace TutorBuddy.Core.Interface
{
	public interface IUnitOfWork : IDisposable
	{
        IUserRepository UserRepository { get; }
        ITutorRepository TutorRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IAvailabilityRepository AvailabilityRepository { get; }
        Task<bool> Save();
    }
}

