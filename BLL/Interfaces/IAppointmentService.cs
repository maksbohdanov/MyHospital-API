using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAppointmentService
    {
        Task MakeAppointmentAsync(NewAppointmentModel model);
    }
}
