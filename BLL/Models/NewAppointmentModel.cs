namespace BLL.Models
{
    public class NewAppointmentModel
    {
        public DateTime Date { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone{ get; set; }
        public int FavorId { get; set; }
        public int DoctorId { get; set; }
    }
}
