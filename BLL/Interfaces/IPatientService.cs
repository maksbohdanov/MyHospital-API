using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> GetByPhoneNumberAsync(string fullName, string phoneNumber);
        Task CreateAsync(string fullName, string phoneNumber);
        Task<bool> CheckIfPatientExistsAsync(string phoneNumber);
    }
}
