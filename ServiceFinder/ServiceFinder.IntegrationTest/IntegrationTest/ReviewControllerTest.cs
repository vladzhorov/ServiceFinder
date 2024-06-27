using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.API.ViewModels.Review;
using ServiceFinder.DAL;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.PaginationObjects;
using ServiceFinder.IntegrationTest;
using ServiceFinder.IntegrationTest.Constants;
using ServiceFinder.IntegrationTests.TestHelpers;
using System.Net.Http.Json;

namespace ServiceFinder.IntegrationTests
{
    public class ReviewControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Program> _factory;
        private readonly ReviewTestHelper _testHelper;
        private readonly Guid _reviewId = Guid.NewGuid();
        private readonly Guid _userProfileId;
        private readonly Guid _assistanceId;
        private readonly Guid _assistanceCategoryId;

        public ReviewControllerTests(TestingWebAppFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            _testHelper = new ReviewTestHelper(factory.Services.GetRequiredService<IServiceScopeFactory>());
            (_userProfileId, _assistanceId, _assistanceCategoryId) = AddTestData();
        }

        public (Guid, Guid, Guid) AddTestData()
        {
            var userProfileId = Guid.NewGuid();
            var assistanceId = Guid.NewGuid();
            var assistanceCategoryId = Guid.NewGuid();
            var reviewId = _reviewId;

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

            var review = new ReviewEntity
            {
                Id = reviewId,
                AssistanceId = assistanceId,
                UserProfileId = userProfileId,
                Rating = 4.5f,
                Comment = "Great service!",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.UserProfile.Add(userProfile);
                dbContext.AssistanceCategories.Add(assistanceCategory);
                dbContext.Assistances.Add(assistance);
                dbContext.Reviews.Add(review);

                dbContext.SaveChanges();
            }

            return (userProfileId, assistanceId, assistanceCategoryId);
        }

        [Fact]
        public async Task GetAll_ReturnsPagedResult()
        {
            var pageNumber = 1;
            var pageSize = 10;

            var response = await _client.GetAsync($"{ApiRoutes.Reviews}?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PagedResult<ReviewViewModel>>() ?? throw new InvalidOperationException();

            _testHelper.AssertPagedResult(result, pageNumber, pageSize);
        }

        [Fact]
        public async Task GetById_ReturnsReviewViewModel()
        {
            var response = await _client.GetAsync($"{ApiRoutes.Reviews}/{_reviewId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ReviewViewModel>() ?? throw new InvalidOperationException();

            _testHelper.AssertReview(result, _reviewId);
        }

        [Fact]
        public async Task Create_ReturnsCreatedReviewViewModel()
        {
            var createViewModel = new CreateReviewViewModel
            {
                AssistanceId = _assistanceId,
                UserProfileId = _userProfileId,
                Rating = 5.0f,
                Comment = "Excellent service!"
            };

            var response = await _client.PostAsJsonAsync(ApiRoutes.Reviews, createViewModel);
            response.EnsureSuccessStatusCode();

            var createdReview = await response.Content.ReadFromJsonAsync<ReviewViewModel>() ?? throw new InvalidOperationException();

            _testHelper.AssertCreatedReview(createdReview, createViewModel);
        }

        [Fact]
        public async Task Delete_RemovesReviewFromDatabase()
        {
            var response = await _client.DeleteAsync($"{ApiRoutes.Reviews}/{_reviewId}");
            response.EnsureSuccessStatusCode();

            _testHelper.AssertReviewDeleted(_reviewId);
        }
    }
}
