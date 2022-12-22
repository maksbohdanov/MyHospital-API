using DAL;
using DAL.Entities;
using DAL.Repositories;
using MyHospital.Tests.Helpers;

namespace MyHospital.Tests.DataTests
{
    [TestFixture]
    internal class AppointmentRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task AppointmentRepository_GetByIdAsync_ReturnsCorrectValue(int id)
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var appointmentRepository = new AppointmentRepository(context);
            var appointment = await appointmentRepository.GetByIdAsync(id);

            var expected = DataHelper.Appointments
                .FirstOrDefault(x => x.Id == id);

            Assert.That(appointment, Is.EqualTo(expected).Using(new AppointmentEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task AppointmentRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var appointmentRepository = new AppointmentRepository(context);
            var appointments = await appointmentRepository.GetAllAsync();

            Assert.That(appointments.OrderBy(x => x.Id), Is.EqualTo(DataHelper.Appointments).Using(new AppointmentEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task AppointmentRepository_FindAsync_ReturnsCorrectValues()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var appointmentRepository = new AppointmentRepository(context);
            var appointment = await appointmentRepository.FindAsync(x => x.DoctorId == 1);

            var expected = DataHelper.Appointments[0];

            Assert.That(appointment.FirstOrDefault(), Is.EqualTo(expected).Using(new AppointmentEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task AppointmentRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var appointmentRepository = new AppointmentRepository(context);
            var appointment = new Appointment()
            {
                Id = 4,
                DoctorId = 1,
                FavorId = 1,
                PatientId = 1,
                Date = DateTime.Today
            };

            await appointmentRepository.AddAsync(appointment);
            await context.SaveChangesAsync();

            Assert.That(context.Favors.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task AppointmentRepository_Update_UpdatesEntity()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var appointmentRepository = new AppointmentRepository(context);
            var appointment = new Appointment()
            {
                Id = 1,
                DoctorId = 1,
                FavorId = 1,
                PatientId = 1,
                Date = DateTime.Today
            };


            appointmentRepository.Update(appointment);
            await context.SaveChangesAsync();


            var updatedPatient = await appointmentRepository.GetByIdAsync(1);
            Assert.That(updatedPatient, Is.EqualTo(new Appointment()
            {
                Id = 1,
                DoctorId = 1,
                FavorId = 1,
                PatientId = 1,
                Date = DateTime.Today
            }).Using(new AppointmentEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task AppointmentRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var appointmentRepository = new AppointmentRepository(context);

            await appointmentRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Appointments.Count(), Is.EqualTo(2), message: "DeleteByIdAsync method works incorrect");
        }
    }
}
