using DAL.Entities;

namespace BLL.Models
{
    public class FavorModel: BaseEntity
    {
        public decimal Price { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public string FavorName { get; set; } = string.Empty;
        public string FavorType { get; set; } = string.Empty;
    }
}
