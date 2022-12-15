using DAL;
using DAL.Entities;
using DAL.Repositories;
using MyHospital.Tests.Helpers;

namespace MyHospital.Tests.DataTests
{
    [TestFixture]
    internal class PatientRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task PatientRepository_GetByIdAsync_ReturnsCorrectValue(int id)
        {
            using var context = new HospitalDbContext(DataTestsHelper.GetHospitalDbOptions());

            var patientRepository = new PatientRepository(context);
            var patient = await patientRepository.GetByIdAsync(id);

            var expected = DataTestsHelper.Patients
                .FirstOrDefault(x => x.Id == id);

            Assert.That(patient, Is.EqualTo(expected).Using(new PatientEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task PatientRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new HospitalDbContext(DataTestsHelper.GetHospitalDbOptions());

            var patientRepository = new PatientRepository(context);
            var patients = await patientRepository.GetAllAsync();

            Assert.That(patients.OrderBy(x => x.Id), Is.EqualTo(DataTestsHelper.Patients).Using(new PatientEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task PatientRepository_FindAsync_ReturnsCorrectValues()
        {
            using var context = new HospitalDbContext(DataTestsHelper.GetHospitalDbOptions());

            var patientRepository = new PatientRepository(context);
            var patient = await patientRepository.FindAsync(x => x.FullName.EndsWith("Петро"));

            var expected = DataTestsHelper.Patients[0];

            Assert.That(patient.FirstOrDefault(), Is.EqualTo(expected).Using(new PatientEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task PatientRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new HospitalDbContext(DataTestsHelper.GetHospitalDbOptions());

            var patientRepository = new PatientRepository(context);
            var patient = new Patient() { Id = 3, FullName="new", Phone = "380680000000"};

            await patientRepository.AddAsync(patient);
            await context.SaveChangesAsync();

            Assert.That(context.Patients.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task PatientRepository_Update_UpdatesEntity()
        {
            using var context = new HospitalDbContext(DataTestsHelper.GetHospitalDbOptions());

            var patientRepository = new PatientRepository(context);
            var patient = new Patient() { Id = 1, FullName = "Updated", Phone = "380680000000" };


            patientRepository.Update(patient);
            await context.SaveChangesAsync();


            var updatedPatient = await patientRepository.GetByIdAsync(1);
            Assert.That(updatedPatient, Is.EqualTo(new Patient
            {
                Id = 1,
                FullName = "Updated",
                Phone = "380680000000"
            }).Using(new PatientEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task PatientRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new HospitalDbContext(DataTestsHelper.GetHospitalDbOptions());

            var patientRepository = new PatientRepository(context);

            await patientRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Patients.Count(), Is.EqualTo(1), message: "DeleteByIdAsync method works incorrect");
        }
    }
}
