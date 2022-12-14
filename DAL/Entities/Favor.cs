namespace DAL.Entities
{
    public class Favor: BaseEntity
    {
        public decimal Prive { get; set; }

        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }

        public int FavorNameId { get; set; }
        public virtual FavorName FavorName { get; set; }

        public int FavorTypeId { get; set; }
        public virtual FavorType FavorType { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
