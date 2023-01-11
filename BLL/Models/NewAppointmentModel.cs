namespace BLL.Models
{
    public class NewAppointmentModel
    {
        public DateTime Date { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientPhone { get; set; } = string.Empty;
        public int FavorId { get; set; }
        public int DoctorId { get; set; }
    }
}
