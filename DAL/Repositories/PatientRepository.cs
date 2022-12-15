using DAL.Entities;

namespace DAL.Repositories
{
    public class PatientRepository : Repository<Patient>
    {
        public PatientRepository(HospitalDbContext context) : base(context)
        {
        }
    }
}
