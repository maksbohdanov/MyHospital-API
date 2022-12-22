using DAL;
using DAL.Entities;
using DAL.Repositories;
using MyHospital.Tests.Helpers;

namespace MyHospital.Tests.DataTests
{
    [TestFixture]
    internal class DoctorRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task DoctorRepository_GetByIdAsync_ReturnsCorrectValue(int id)
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var doctorRepository = new DoctorRepository(context);
            var doctor = await doctorRepository.GetByIdAsync(id);

            var expected = DataHelper.Doctors
                .FirstOrDefault(x => x.Id == id);

            Assert.That(doctor, Is.EqualTo(expected).Using(new DoctorEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task DoctorRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var doctorRepository = new DoctorRepository(context);
            var doctors = await doctorRepository.GetAllAsync();

            Assert.That(doctors.OrderBy(x => x.Id), Is.EqualTo(DataHelper.Doctors).Using(new DoctorEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task DoctorRepository_FindAsync_ReturnsCorrectValues()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var doctorRepository = new DoctorRepository(context);
            var doctor = await doctorRepository.FindAsync(x => x.Experience > 14);

            var expected = DataHelper.Doctors[2];

            Assert.That(doctor.FirstOrDefault(), Is.EqualTo(expected).Using(new DoctorEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task DoctorRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var doctorRepository = new DoctorRepository(context);
            var doctor = new Doctor() 
            {
                Id = 5,
                FirstName="FirstName new", 
                LastName = "LastName new",
                MiddleName = "MiddleName new",
                Experience = 8,
                SpecializationId = 2
            };

            await doctorRepository.AddAsync(doctor);
            await context.SaveChangesAsync();

            Assert.That(context.Doctors.Count(), Is.EqualTo(5), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task DoctorRepository_Update_UpdatesEntity()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var doctorRepository = new DoctorRepository(context);
            var doctor = new Doctor()
            {
                Id = 1,
                FirstName = "FirstName new",
                LastName = "LastName new",
                MiddleName = "MiddleName new",
                Experience = 8,
                SpecializationId = 2
            };


            doctorRepository.Update(doctor);
            await context.SaveChangesAsync();


            var updatedPatient = await doctorRepository.GetByIdAsync(1);
            Assert.That(updatedPatient, Is.EqualTo(new Doctor()
            {
                Id = 1,
                FirstName = "FirstName new",
                LastName = "LastName new",
                MiddleName = "MiddleName new",
                Experience = 8,
                SpecializationId = 2
            }).Using(new DoctorEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task DoctorRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var doctorRepository = new DoctorRepository(context);

            await doctorRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Doctors.Count(), Is.EqualTo(3), message: "DeleteByIdAsync method works incorrect");
        }
    }
}
