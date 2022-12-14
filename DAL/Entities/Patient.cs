namespace DAL.Entities
{
    public class Patient: BaseEntity
    {
        public string FullName{ get; set; }
        public string Phone{ get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
