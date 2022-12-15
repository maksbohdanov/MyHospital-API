using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Doctor> Doctors { get; }
        IRepository<Appointment> Appointments { get; }
        IRepository<Patient> Patients { get; }
        IRepository<Favor> Favors { get; }

        Task SaveChangesAsync();
    }
}
