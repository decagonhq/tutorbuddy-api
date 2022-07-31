using System;
using Microsoft.EntityFrameworkCore;
using TutorBuddy.Core.Interface;
using TutorialBuddy.Infastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TutorBuddyContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(TutorBuddyContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await SaveAsync();
        }

        public async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return await SaveAsync();
        }

        public async Task<IEnumerable<T>> GetAllRecord()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetARecord(string Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            return await SaveAsync();
        }

        public async Task<bool> UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}

