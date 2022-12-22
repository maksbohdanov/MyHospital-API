using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using Moq;
using MyHospital.Tests.Helpers;
using System.Linq.Expressions;

namespace MyHospital.Tests.BusinessTests
{
    [TestFixture]
    internal class FavorServiceTests
    {
        private IFavorService _favorService;
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _favorService = new FavorService(_unitOfWork.Object, MapperHelper.CreateMapperProfile());
        }

        [Test]
        public async Task FavorService_GetByIdAsync_ReturnsFavorModel()
        {
            var expected = new FavorModel()
            {
                Id = 1,
                Price= 500,
                Specialization = "Дослідження та діагностика",
                FavorName = "Ультразвукова діагностика",
                FavorType = "Діагностика"
            };
            _unitOfWork.Setup(x => x.Favors.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(DataHelper.Favors[0]);

            var actual = await _favorService.GetByIdAsync(1);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FavorService_GetByIdAsync_ThrowsExceptionl()
        {
            _unitOfWork.Setup(x => x.Favors.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Favor>());

            Func<Task> act = async () => await _favorService.GetByIdAsync(It.IsAny<int>());

            await act.Should().ThrowAsync<HospitalException>();
        }

        [Test]
        public async Task FavorService_GetAllAsync_ReturnsAllFavors()
        {
            _unitOfWork.Setup(x => x.Favors.GetAllAsync())
                .ReturnsAsync(DataHelper.Favors);

            var favors = await _favorService.GetAllAsync();

            _unitOfWork.VerifyAll();
            Assert.That(favors.Count(), Is.EqualTo(4));
        }

        [TestCase("Дослідження та діагностика")]
        public async Task FavorService_FindBySpecializationAsync_ReturnsCorrectValues(string specialization)
        {
            _unitOfWork.Setup(x => x.Favors.FindAsync(It.IsAny<Expression<Func<Favor, bool>>>()))
                .ReturnsAsync(DataHelper.Favors.Where(x => x.Specialization != null && x.Specialization.Title == specialization));

            var favors = await _favorService.FindBySpecializationAsync(specialization);

            _unitOfWork.VerifyAll();
            Assert.That(favors.Count(), Is.EqualTo(1));
        }
    }
}
