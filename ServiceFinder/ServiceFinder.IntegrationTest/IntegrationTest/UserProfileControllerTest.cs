using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ServiceFinder.API.ViewModels.UserProfile;
using ServiceFinder.DAL;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.PaginationObjects;
using ServiceFinder.IntegrationTest;
using ServiceFinder.IntegrationTest.Constants;
using ServiceFinder.IntegrationTests.TestHelpers;
using System.Net.Http.Json;

namespace ServiceFinder.IntegrationTests
{
    public class UserProfileControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Program> _factory;
        private readonly UserProfileTestHelper _testHelper;
        private readonly Guid _userProfileId = Guid.NewGuid();

        public UserProfileControllerTest(TestingWebAppFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
            _testHelper = new UserProfileTestHelper(factory.Services.GetRequiredService<IServiceScopeFactory>());
            AddTestData();
        }

        private void AddTestData()
        {
            var userProfile = new UserProfileEntity
            {
                Id = _userProfileId,
                PhotoURL = "test/photos/url",
                PhoneNumber = "8(029)221-45-67",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.UserProfile.Add(userProfile);
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAll_ShouldReturnPagedResult()
        {
            var response = await _client.GetAsync($"{ApiRoutes.UserProfiles}?pageNumber=1&pageSize=10");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PagedResult<UserProfileViewModel>>(content);
            _testHelper.AssertPagedResult(result!, 1, 10);
        }

        [Fact]
        public async Task GetById_ShouldReturnUserProfile()
        {
            var response = await _client.GetAsync($"{ApiRoutes.UserProfiles}/{_userProfileId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserProfileViewModel>(content);
            _testHelper.AssertUserProfile(result!, _userProfileId);
        }

        [Fact]
        public async Task Create_ShouldCreateUserProfile()
        {
            var viewModel = UserProfileTestHelper.CreateUserProfileViewModel();
            var response = await _client.PostAsJsonAsync(ApiRoutes.UserProfiles, viewModel);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserProfileViewModel>(content);
            _testHelper.AssertCreatedUserProfile(result!, viewModel);
        }

        [Fact]
        public async Task Update_ShouldUpdateUserProfile()
        {
            var viewModel = UserProfileTestHelper.UpdateUserProfileViewModel();
            var response = await _client.PutAsJsonAsync($"{ApiRoutes.UserProfiles}/{_userProfileId}", viewModel);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserProfileViewModel>(content);
            _testHelper.AssertUpdatedUserProfile(result!, _userProfileId, viewModel);
        }

        [Fact]
        public async Task Delete_ShouldDeleteUserProfile()
        {
            var response = await _client.DeleteAsync($"{ApiRoutes.UserProfiles}/{_userProfileId}");
            response.EnsureSuccessStatusCode();
            _testHelper.AssertUserProfileDeleted(_userProfileId);
        }
    }
}