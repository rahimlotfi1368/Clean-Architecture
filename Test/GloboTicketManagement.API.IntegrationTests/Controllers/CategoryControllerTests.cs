using GlobalTicketManagement.Api;
using GlobalTicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using GloboTicketManagement.API.IntegrationTests.Base;
using System.Text.Json;

namespace GloboTicketManagement.API.IntegrationTests.Controllers
{
    public class CategoryControllerTests:IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public CategoryControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/Category/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CategoryListVm>>(responseString);

            Assert.IsType<List<CategoryListVm>>(result);

            Assert.NotEmpty(result);
        }
    }
}