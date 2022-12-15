using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class FavorRepository : Repository<Favor>
    {
        public FavorRepository(HospitalDbContext context) : base(context)
        {
        }

        public async override Task<Favor?> GetByIdAsync(int id)
        {
            return (await GetAllAsync())
                .FirstOrDefault(x => x.Id == id);
        }

        public async override Task<IEnumerable<Favor>> GetAllAsync()
        {
            return await _context.Favors
                .Include(x => x.FavorName)
                .Include(x => x.FavorType)
                .Include(x => x.Specialization)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Favor>> FindAsync(Expression<Func<Favor, bool>> predicate)
        {
            return (await GetAllAsync())
                .Where(predicate.Compile());
        }
    }
}
