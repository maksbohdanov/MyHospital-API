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
    internal class PatientServiceTests
    {
        private IPatientService _patientService;
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _patientService = new PatientService(_unitOfWork.Object);
        }

        [Test]
        public async Task PatientService_CreateAsync_AddsPatient()
        {
            _unitOfWork.Setup(x => x.Patients.AddAsync(It.IsAny<Patient>()))
                .Verifiable();

            await _patientService.CreateAsync("Name", "Phone");

            _unitOfWork.Verify(x => x.Patients.AddAsync(It.IsAny<Patient>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task PatientService_CheckIfPatientExistsAsync_ReturnsTrueIfExists()
        {
            _unitOfWork.Setup(x => x.Patients.GetAllAsync())
                .ReturnsAsync(DataHelper.Patients);

            var result = await _patientService.CheckIfPatientExistsAsync("380961234567");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task PatientService_GetByPhoneNumberAsync_ReturnsPatient()
        {
            _unitOfWork.Setup(x => x.Patients.GetAllAsync())
                .ReturnsAsync(DataHelper.Patients);
            _unitOfWork.Setup(x => x.Patients.FindAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                .ReturnsAsync(DataHelper.Patients);

            var expected = new Patient() { Id = 1, FullName = "Петренко Петро", Phone = "380961234567" };

            var actual = await _patientService.GetByPhoneNumberAsync("Петренко Петро", "380961234567");

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
