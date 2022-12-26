using BLL.Models;
using FluentAssertions;
using MyHospital.Tests.Helpers;
using Newtonsoft.Json;
using System.Net;

namespace MyHospital.Tests.IntegrationTests
{
    [TestFixture]
    internal class FavorsControllerTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private const string RequestUri = "api/favors/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task FavorsController_GetById_ReturnsFavorById()
        {
            var expected = new FavorModel()
            {
                Id = 1,
                Price = 500,
                Specialization = "Дослідження та діагностика",
                FavorName = "Ультразвукова діагностика",
                FavorType = "Діагностика"
            };
            var favorId = 1;

            var httpResponse = await _client.GetAsync(RequestUri + favorId);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<FavorModel>(stringResponse);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FavorsController_GetById_ReturnsNotFound()
        {
            var favorId = -1;

            var httpResponse = await _client.GetAsync(RequestUri + favorId);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task FavorsController_GetAll_ReturnsAllValues()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<FavorModel>>(stringResponse).ToList();

            Assert.That(actual.Count, Is.EqualTo(4));
        }

        [TestCase("дослідження")]
        public async Task FavorsController_FindByFilter_ReturnsFavorBySpecialization(string specialization)
        {
            var expected = new FavorModel()
            {
                Id = 1,
                Price = 500,
                Specialization = "Дослідження та діагностика",
                FavorName = "Ультразвукова діагностика",
                FavorType = "Діагностика"
            };

            var httpResponse = await _client.GetAsync(RequestUri + "specialization/" + specialization);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<FavorModel>>(stringResponse);

            actual.First().Should().BeEquivalentTo(expected);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
