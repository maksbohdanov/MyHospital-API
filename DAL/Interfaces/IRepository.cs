﻿using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task CreateAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        Task<bool> DeleteByIdAsync(int id);
    }
}
