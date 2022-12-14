namespace DAL.Entities
{
    public class Specialization: BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public virtual ICollection<Favor> Favors { get; set; } = new HashSet<Favor>();
        public virtual ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();
    }
}