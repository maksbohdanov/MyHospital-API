using DAL.Entities;

namespace DAL.Repositories
{
    public class AppointmentRepository : Repository<Appointment>
    {
        public AppointmentRepository(HospitalDbContext context) : base(context)
        {
        }
    }
}
