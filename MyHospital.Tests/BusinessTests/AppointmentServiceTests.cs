using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;
using MyHospital.Tests.Helpers;

namespace MyHospital.Tests.BusinessTests
{
    [TestFixture]
    internal class AppointmentServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IPatientService> _patientService;
        private IAppointmentService _appointmentService;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _patientService= new Mock<IPatientService>();
            _appointmentService = new AppointmentService(_unitOfWork.Object, _patientService.Object);
        }

        [Test]
        public async Task AppointmentService_MakeAppointmentAsync_AddsNewAppointment()
        {
            var model = new NewAppointmentModel()
            {
                Date = DateTime.Today,
                PatientName = "Петренко Петро",
                PatientPhone = "380961234567",
                DoctorId = 1,
                FavorId = 1,
            };
            _patientService.Setup(x => x.GetByPhoneNumberAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(DataHelper.Patients.First());
            _unitOfWork.Setup(x => x.Appointments.AddAsync(It.IsAny<Appointment>()))
                .Verifiable();

            await _appointmentService.MakeAppointmentAsync(model);

            _unitOfWork.Verify(x => x.Appointments.AddAsync(It.IsAny<Appointment>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
