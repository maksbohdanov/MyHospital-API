using BLL.Models;

namespace BLL.Interfaces
{
    public interface IFavorService
    {
        Task<FavorModel> GetByIdAsync(int id);
        Task<IEnumerable<FavorModel>> GetAllAsync();
        Task<IEnumerable<FavorModel>> FindBySpecializationAsync(string specialization);
    }
}
