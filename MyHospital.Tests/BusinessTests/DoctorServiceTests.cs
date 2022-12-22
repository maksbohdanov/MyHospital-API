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
    internal class DoctorServiceTests
    {
        private IDoctorService _doctorService;
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _doctorService = new DoctorService(_unitOfWork.Object, MapperHelper.CreateMapperProfile());
        }

        [Test]
        public async Task DoctorService_GetByIdAsync_ReturnsDoctorModel()
        {
            var expected = new DoctorModel()
            {
                Id = 1,
                FirstName = "Анна",
                LastName = "Мельник",
                MiddleName = "Олексіївна",
                Experience = 3,
                Specialization = "Дослідження та діагностика"
            };
            _unitOfWork.Setup(x => x.Doctors.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(DataHelper.Doctors[0]);

            var actual = await _doctorService.GetByIdAsync(1);

            actual.Should().BeEquivalentTo(expected, o => o
                .Excluding(x => x.Appointments));
        }

        [Test]
        public async Task DoctorService_GetByIdAsync_ThrowsExceptionl()
        {
            _unitOfWork.Setup(x => x.Doctors.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Doctor>());

            Func<Task> act = async () => await _doctorService.GetByIdAsync(It.IsAny<int>());

            await act.Should().ThrowAsync<HospitalException>();
        }

        [Test]
        public async Task DoctorService_GetAllAsync_ReturnsAllDoctors()
        {
            _unitOfWork.Setup(x => x.Doctors.GetAllAsync())
                .ReturnsAsync(DataHelper.Doctors);

            var doctors = await _doctorService.GetAllAsync();

            _unitOfWork.VerifyAll();
            Assert.That(doctors.Count(), Is.EqualTo(4));
        }

        [TestCase("Мельник")]
        public async Task DoctorService_FindByFilterAsync_ReturnsCorrectValues(string filter)
        {
            _unitOfWork.Setup(x => x.Doctors.FindAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                .ReturnsAsync(DataHelper.Doctors.Where(x => x.LastName == filter));

            var doctors = await _doctorService.FindByFilterAsync(filter);

            _unitOfWork.VerifyAll();
            Assert.That(doctors.Count(), Is.EqualTo(1));
        }
    }
}
