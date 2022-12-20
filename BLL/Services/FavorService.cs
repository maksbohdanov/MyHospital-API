using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class FavorService: IFavorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FavorModel> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.Favors.GetByIdAsync(id);
            if (result == null)
            {
                throw new HospitalException("Favor with specified id was not found.");
            }
            return _mapper.Map<FavorModel>(result);
        }

        public async Task<IEnumerable<FavorModel>> GetAllAsync()
        {
            var favors = await _unitOfWork.Favors.GetAllAsync();
            return _mapper.Map<IEnumerable<FavorModel>>(favors);
        }

        public async Task<IEnumerable<FavorModel>> FindBySpecializationAsync(string specialization)
        {
            var favors = await _unitOfWork.Favors.FindAsync(x =>                
                x.Specialization.Title.ToLower().Contains(specialization.ToLower()));

            return _mapper.Map<IEnumerable<FavorModel>>(favors);
        }
    }
}
