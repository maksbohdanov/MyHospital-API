using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class AppointmentService: IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientService _patientService;

        public AppointmentService(IUnitOfWork unitOfWork, IPatientService patientService)
        {
            _unitOfWork = unitOfWork;
            _patientService = patientService;
        }

        public async Task MakeAppointmentAsync(NewAppointmentModel model)
        {
            if (model.FavorId > 4)
                model.DoctorId = 1;

            var patient = await _patientService.GetByPhoneNumberAsync(model.PatientName, model.PatientPhone);

            var appointment = new Appointment()
            {
                Date = model.Date,
                DoctorId = model.DoctorId,
                FavorId = model.FavorId,
                PatientId = patient.Id
            };
            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
