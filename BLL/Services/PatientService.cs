using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PatientService: IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Patient> GetByPhoneNumberAsync(string fullName, string phoneNumber)
        {
            if(!await CheckIfPatientExistsAsync(phoneNumber))
            {
                await CreateAsync(fullName, phoneNumber);                
            }
            var patients = await _unitOfWork.Patients.FindAsync(x => x.Phone == phoneNumber);
            return patients.First();
        }

        public async Task CreateAsync(string fullName, string phoneNumber)
        {
            var patient =new Patient() { FullName = fullName, Phone = phoneNumber };
            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CheckIfPatientExistsAsync(string phoneNumber)
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();
            return patients.Any(x => x.Phone == phoneNumber);
        }
    }
}
