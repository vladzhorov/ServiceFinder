using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.API.ViewModels.UserProfile;
using ServiceFinder.DAL;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.IntegrationTests.TestHelpers
{
    public class UserProfileTestHelper
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public UserProfileTestHelper(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void AssertPagedResult(PagedResult<UserProfileViewModel> result, int pageNumber, int pageSize)
        {
            result.Should().NotBeNull();
            result.Data.Should().NotBeNullOrEmpty();
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
        }

        public void AssertUserProfile(UserProfileViewModel result, Guid id)
        {
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        public void AssertCreatedUserProfile(UserProfileViewModel result, CreateUserProfileViewModel viewModel)
        {
            result.Should().NotBeNull();
            result.PhotoURL.Should().Be(viewModel.PhotoURL);
            result.PhoneNumber.Should().Be(viewModel.PhoneNumber);
        }

        public void AssertUpdatedUserProfile(UserProfileViewModel result, Guid id, UpdateUserProfileViewModel viewModel)
        {
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.PhotoURL.Should().Be(viewModel.PhotoURL);
            result.PhoneNumber.Should().Be(viewModel.PhoneNumber);
        }

        public void AssertUserProfileDeleted(Guid id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var deletedUserProfile = dbContext.UserProfile.Find(id);
                deletedUserProfile.Should().BeNull();
            }
        }

        public static CreateUserProfileViewModel CreateUserProfileViewModel()
        {
            return new CreateUserProfileViewModel
            {
                PhotoURL = "new/photo/url",
                PhoneNumber = "8(029)111-45-67",
            };
        }

        public static UpdateUserProfileViewModel UpdateUserProfileViewModel()
        {
            return new UpdateUserProfileViewModel
            {
                PhotoURL = "updated/photo/url",
                PhoneNumber = "8(029)121-45-67",
            };
        }
    }
}
