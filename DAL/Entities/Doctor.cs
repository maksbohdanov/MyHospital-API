namespace DAL.Entities
{
    public class Doctor: BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public int Experience { get; set; }

        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}