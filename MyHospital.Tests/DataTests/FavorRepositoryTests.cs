using DAL;
using DAL.Entities;
using DAL.Repositories;
using MyHospital.Tests.Helpers;

namespace MyHospital.Tests.DataTests
{
    [TestFixture]
    internal class FavorRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task FavorRepository_GetByIdAsync_ReturnsCorrectValue(int id)
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var favorRepository = new FavorRepository(context);
            var favor = await favorRepository.GetByIdAsync(id);

            var expected = DataHelper.Favors
                .FirstOrDefault(x => x.Id == id);

            Assert.That(favor, Is.EqualTo(expected).Using(new FavorEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task FavorRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var favorRepository = new FavorRepository(context);
            var favors = await favorRepository.GetAllAsync();

            Assert.That(favors.OrderBy(x => x.Id), Is.EqualTo(DataHelper.Favors).Using(new FavorEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task FavorRepository_FindAsync_ReturnsCorrectValues()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var favorRepository = new FavorRepository(context);
            var favor = await favorRepository.FindAsync(x => x.Price < 350);

            var expected = DataHelper.Favors[3];

            Assert.That(favor.FirstOrDefault(), Is.EqualTo(expected).Using(new FavorEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task FavorRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var favorRepository = new FavorRepository(context);
            var favor = new Favor() 
            {
                Id = 5,
                SpecializationId= 1, 
                FavorTypeId = 1,
                FavorNameId = 1,
                Price = 333
            };

            await favorRepository.AddAsync(favor);
            await context.SaveChangesAsync();

            Assert.That(context.Favors.Count(), Is.EqualTo(5), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task FavorRepository_Update_UpdatesEntity()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var favorRepository = new FavorRepository(context);
            var favor = new Favor()
            {
                Id = 1,
                SpecializationId = 1,
                FavorTypeId = 1,
                FavorNameId = 1,
                Price = 333
            };


            favorRepository.Update(favor);
            await context.SaveChangesAsync();


            var updatedPatient = await favorRepository.GetByIdAsync(1);
            Assert.That(updatedPatient, Is.EqualTo(new Favor()
            {
                Id = 1,
                SpecializationId = 1,
                FavorTypeId = 1,
                FavorNameId = 1,
                Price = 333
            }).Using(new FavorEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task FavorRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new HospitalDbContext(DataHelper.GetHospitalDbOptions());

            var favorRepository = new FavorRepository(context);

            await favorRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Favors.Count(), Is.EqualTo(3), message: "DeleteByIdAsync method works incorrect");
        }
    }
}
