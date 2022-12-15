using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly HospitalDbContext _context;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Favor> _favorRepository;

        public UnitOfWork(HospitalDbContext context, IRepository<Doctor> doctorRepository,
            IRepository<Appointment> appointmentRepository, IRepository<Patient> patientRepository,
            IRepository<Favor> favorRepository)
        {
            _context = context;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _favorRepository = favorRepository;
        }

        public IRepository<Doctor> Doctors => _doctorRepository;
        public IRepository<Appointment> Appointments => _appointmentRepository;
        public IRepository<Patient> Patients => _patientRepository;
        public IRepository<Favor> Favors => _favorRepository;


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
