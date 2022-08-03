namespace TutorBuddy.Core.Interface
{
	public interface IGenericRepository<T>
    {
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllRecord();
        Task<T> GetARecord(string Id);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}

