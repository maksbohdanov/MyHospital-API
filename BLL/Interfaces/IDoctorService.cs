using BLL.Models;

namespace BLL.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorModel> GetByIdAsync(int id);
        Task<IEnumerable<DoctorModel>> GetAllAsync();
        Task<IEnumerable<DoctorModel>> FindByFilterAsync(string filter);
    }
}
