using DAL.Entities;

namespace BLL.Models
{
    public class DoctorModel: BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public int Experience { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public ICollection<DateTime> Appointments { get; set; } = new List<DateTime>();
    }
}
