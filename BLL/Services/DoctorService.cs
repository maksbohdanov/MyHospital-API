using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DoctorModel> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (result == null)
            {
                throw new HospitalException("Doctor with specified id was not found.");
            }
            return _mapper.Map<DoctorModel>(result);
        }


        public async Task<IEnumerable<DoctorModel>> GetAllAsync()
        {
            var doctors = await _unitOfWork.Doctors.GetAllAsync();
            return _mapper.Map<IEnumerable<DoctorModel>>(doctors);
        }

        public async Task<IEnumerable<DoctorModel>> FindByFilterAsync(string filter)
        {
            var doctors = await _unitOfWork.Doctors.FindAsync(x => 
                x.FirstName.ToLower().Contains(filter.ToLower()) ||
                x.LastName.ToLower().Contains(filter.ToLower()) ||
                x.MiddleName.ToLower().Contains(filter.ToLower()) ||
                x.Specialization.Title.ToLower().Contains(filter.ToLower()));

            return _mapper.Map<IEnumerable<DoctorModel>>(doctors);
        }
    }
}
