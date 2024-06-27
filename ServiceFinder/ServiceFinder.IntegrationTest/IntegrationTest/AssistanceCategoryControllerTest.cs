using ServiceFinder.API.ViewModels.AssistanceCategory;
using ServiceFinder.DAL.PaginationObjects;
using ServiceFinder.IntegrationTest.Constants;
using ServiceFinder.IntegrationTests.TestHelpers;
using System.Net.Http.Json;

namespace ServiceFinder.IntegrationTest.IntegrationTest
{
    public class AssistanceCategoryControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Program> _factory;
        private readonly AssistanceCategoryTestHelper _testHelper;

        public AssistanceCategoryControllerTests(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
            _testHelper = new AssistanceCategoryTestHelper(factory);
        }

        [Fact]
        public async Task GetAll_ShouldReturnPagedResult()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;

            // Act
            var response = await _client.GetAsync($"{ApiRoutes.AssistanceCategories}?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PagedResult<AssistanceCategoryViewModel>>() ?? throw new InvalidOperationException();

            // Assert
            _testHelper.AssertPagedResult(result, pageNumber, pageSize);
        }

        [Fact]
        public async Task GetById_ShouldReturnAssistanceCategory()
        {
            // Arrange
            var category = _testHelper.GetFirstCategory();

            // Act
            var response = await _client.GetAsync($"{ApiRoutes.AssistanceCategories}/{category.Id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AssistanceCategoryViewModel>() ?? throw new InvalidOperationException();

            // Assert
            _testHelper.AssertAssistanceCategory(result, category.Id);
        }

        [Fact]
        public async Task Create_ShouldCreateAssistanceCategory()
        {
            // Arrange
            var viewModel = AssistanceCategoryTestHelper.CreateAssistanceCategoryViewModel();

            // Act
            var response = await _client.PostAsJsonAsync(ApiRoutes.AssistanceCategories, viewModel);
            response.EnsureSuccessStatusCode();

            var createdCategory = await response.Content.ReadFromJsonAsync<AssistanceCategoryViewModel>() ?? throw new InvalidOperationException();

            // Assert
            _testHelper.AssertCreatedAssistanceCategory(createdCategory, viewModel);
        }

        [Fact]
        public async Task Update_ShouldUpdateAssistanceCategory()
        {
            // Arrange
            var category = _testHelper.GetFirstCategory();
            var updateViewModel = AssistanceCategoryTestHelper.UpdateAssistanceCategoryViewModel();

            // Act
            var response = await _client.PutAsJsonAsync($"{ApiRoutes.AssistanceCategories}/{category.Id}", updateViewModel);
            response.EnsureSuccessStatusCode();

            var updatedCategory = await response.Content.ReadFromJsonAsync<AssistanceCategoryViewModel>() ?? throw new InvalidOperationException();

            // Assert
            _testHelper.AssertUpdatedAssistanceCategory(updatedCategory, category.Id, updateViewModel);
        }

        [Fact]
        public async Task Delete_ShouldDeleteAssistanceCategory()
        {
            // Arrange
            var category = _testHelper.GetFirstCategory();

            // Act
            var response = await _client.DeleteAsync($"{ApiRoutes.AssistanceCategories}/{category.Id}");
            response.EnsureSuccessStatusCode();

            // Assert
            _testHelper.AssertCategoryDeleted(category.Id);
        }
    }
}
