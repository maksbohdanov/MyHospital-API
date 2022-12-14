namespace DAL.Entities
{
    public class Appointment:BaseEntity
    {
        public DateTime Date { get; set; }

        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public int FavorId { get; set; }
        public virtual Favor Favor { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
