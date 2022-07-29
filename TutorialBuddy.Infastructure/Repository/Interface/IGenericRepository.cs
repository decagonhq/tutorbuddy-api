﻿using System;
namespace TutorBuddy.Infrastructure.Repository.Interface
{
	public interface IGenericRepository<T>
    {
        Task<bool> Add(T entity);
        Task<bool> AddRange(IEnumerable<T> entities);
        Task<bool> Delete(T entity);
        Task<bool> DeleteRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllRecord();
        Task<T> GetARecord(string Id);
        Task<bool> Update(T entity);
        Task<bool> UpdateRange(IEnumerable<T> entities);
    }
}

