using BLL.Models;
using Castle.Core.Resource;
using DAL;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MyHospital.Tests.Helpers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyHospital.Tests.IntegrationTests
{
    [TestFixture]
    internal class AppointmentsControllerTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private const string RequestUri = "api/appointments/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task AppointmentsController_MakeAppointment_WorksCorrectly()
        {
            var model = new NewAppointmentModel()
            {
                Date = DateTime.Today,
                PatientName = "Петренко Петро",
                PatientPhone = "380961234567",
                DoctorId = 1,
                FavorId = 1,
            };

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            httpResponse.EnsureSuccessStatusCode();

            CheckAppointmentInfoIntoDb(4);
        }

        private void CheckAppointmentInfoIntoDb(int expectedLength)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<HospitalDbContext>();
            context.Appointments.Should().HaveCount(expectedLength);

            var dbEntity = context.Appointments.Last();
            dbEntity.Should().NotBeNull();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
