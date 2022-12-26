using BLL.Models;
using FluentAssertions;
using MyHospital.Tests.Helpers;
using Newtonsoft.Json;
using System.Net;

namespace MyHospital.Tests.IntegrationTests
{
    [TestFixture]
    internal class DoctorsControllerTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private const string RequestUri = "api/doctors/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task DoctorsController_GetById_ReturnsDoctorById()
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
            var customerId = 1;

            var httpResponse = await _client.GetAsync(RequestUri + customerId);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<DoctorModel>(stringResponse);

            actual.Should().BeEquivalentTo(expected, o => o
                .Excluding(x => x.Appointments));
        }

        [Test]
        public async Task DoctorsController_GetById_ReturnsNotFound()
        {
            var customerId = -1;

            var httpResponse = await _client.GetAsync(RequestUri + customerId);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task DoctorsController_GetAll_ReturnsAllValues()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<DoctorModel>>(stringResponse).ToList();

            Assert.That(actual.Count, Is.EqualTo(4));
        }

        [TestCase("анна")]
        [TestCase("мельн")]
        [TestCase("дослідження")]
        public async Task DoctorsController_FindByFilter_ReturnsDoctorByFilter(string filter)
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

            var httpResponse = await _client.GetAsync(RequestUri +"filter/" + filter);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<DoctorModel>>(stringResponse);

            actual.First().Should().BeEquivalentTo(expected, o => o
                .Excluding(x => x.Appointments));
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
