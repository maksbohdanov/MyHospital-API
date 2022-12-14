using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity: BaseEntity 
    {
        protected readonly HospitalDbContext _context;

        protected Repository(HospitalDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>()
                .ToListAsync();
        }

        public virtual async  Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            return true;
        }
    }
}
