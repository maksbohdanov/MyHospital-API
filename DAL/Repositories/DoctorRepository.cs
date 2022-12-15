using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class DoctorRepository : Repository<Doctor>
    {
        public DoctorRepository(HospitalDbContext context) : base(context)
        {
        }

        public override async Task<Doctor?> GetByIdAsync(int id)
        {
            return (await GetAllAsync())
                .FirstOrDefault(x => x.Id == id);
        }

        public override async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                .Include(x => x.Specialization)
                .Include(x => x.Appointments)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Doctor>> FindAsync(Expression<Func<Doctor, bool>> predicate)
        {
            return (await GetAllAsync())
                .Where(predicate.Compile());
        }
    }
}
