using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.API.ViewModels.Assistance;
using ServiceFinder.DAL;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.PaginationObjects;
using ServiceFinder.IntegrationTest;
using ServiceFinder.IntegrationTest.Constants;
using ServiceFinder.IntegrationTests.TestHelpers;
using System.Net.Http.Json;
namespace ServiceFinder.IntegrationTests
{
    public class AssistanceControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Program> _factory;
        private readonly AssistanceTestHelper _testHelper;
        private readonly Guid _assistanceId = Guid.NewGuid();
        private readonly Guid userProfileId;
        private readonly Guid assistanceCategoryId;

        public AssistanceControllerTests(TestingWebAppFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            _testHelper = new AssistanceTestHelper(factory.Services.GetRequiredService<IServiceScopeFactory>());
            (userProfileId, assistanceCategoryId) = AddTestData();
        }

        public (Guid, Guid) AddTestData()
        {
            var userProfileId = Guid.NewGuid();
            var assistanceCategoryId = Guid.NewGuid();
            var assistanceId = _assistanceId;

            var userProfile = new UserProfileEntity
            {
                Id = userProfileId,
                PhotoURL = "test/photo/url",
                PhoneNumber = "8(029)111-45-67",
                Rating = 4.5f,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var assistanceCategory = new AssistanceCategoryEntity
            {
                Id = assistanceCategoryId,
                Name = "Test Category",
                Description = "Description for Test Category",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var assistance = new AssistanceEntity
            {
                Id = assistanceId,
                Title = "Test Assistance",
                Description = "Description for Test Assistance",
                Price = 100.0m,
                DurationInMinutes = 60,
                Location = "Test Location",
                UserProfileId = userProfileId,
                AssistanceCategoryId = assistanceCategoryId
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.UserProfile.AddRange(userProfile);
                dbContext.AssistanceCategories.AddRange(assistanceCategory);
                dbContext.Assistances.AddRange(assistance);

                dbContext.SaveChanges();
            }

            return (userProfileId, assistanceCategoryId);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfAssistanceViewModels()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;

            // Act
            var response = await _client.GetAsync($"{ApiRoutes.Assistances}?pageNumber={pageNumber}&pageSize={pageSize}");
            var result = await response.Content.ReadFromJsonAsync<PagedResult<AssistanceViewModel>>();

            // Assert
            response.EnsureSuccessStatusCode();
            _testHelper.AssertPagedResult(result!, pageNumber, pageSize);
        }

        [Fact]
        public async Task GetById_ReturnsAssistanceViewModel()
        {
            // Arrange
            var assistanceId = _assistanceId;

            // Act
            var response = await _client.GetAsync($"{ApiRoutes.Assistances}/{assistanceId}");
            var result = await response.Content.ReadFromJsonAsync<AssistanceViewModel>();

            // Assert
            response.EnsureSuccessStatusCode();
            _testHelper.AssertAssistance(result!, assistanceId);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAssistanceViewModel()
        {
            // Arrange
            var createViewModel = new CreateAssistanceViewModel
            {
                Title = "New Assistance",
                Description = "Description for new Assistance",
                Price = 150.0m,
                DurationInMinutes = 90,
                Location = "New Location",
                UserProfileId = userProfileId,
                AssistanceCategoryId = assistanceCategoryId
            };

            // Act
            var response = await _client.PostAsJsonAsync($"{ApiRoutes.Assistances}", createViewModel);
            var createdAssistance = await response.Content.ReadFromJsonAsync<AssistanceViewModel>();

            // Assert
            response.EnsureSuccessStatusCode();
            _testHelper.AssertCreatedAssistance(createdAssistance!, createViewModel);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedAssistanceViewModel()
        {
            // Arrange
            var updateViewModel = new UpdateAssistanceViewModel
            {
                Title = "Updated Assistance",
                Description = "Updated Description",
                Price = 200.0m,
                DurationInMinutes = 120,
                Location = "Updated Location"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"{ApiRoutes.Assistances}/{_assistanceId}", updateViewModel);
            var updatedAssistance = await response.Content.ReadFromJsonAsync<AssistanceViewModel>();

            // Assert
            response.EnsureSuccessStatusCode();
            _testHelper.AssertUpdatedAssistance(updatedAssistance!, _assistanceId, updateViewModel);
        }

        [Fact]
        public async Task Delete_RemovesAssistanceFromDatabase()
        {
            // Act
            var response = await _client.DeleteAsync($"{ApiRoutes.Assistances}/{_assistanceId}");

            // Assert
            response.EnsureSuccessStatusCode();
            _testHelper.AssertAssistanceDeleted(_assistanceId);
        }
    }
}
