using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.API.ViewModels.Review;
using ServiceFinder.DAL;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.IntegrationTests.TestHelpers
{
    public class ReviewTestHelper
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ReviewTestHelper(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public static CreateReviewViewModel CreateReviewViewModel()
        {
            return new CreateReviewViewModel
            {
                AssistanceId = Guid.NewGuid(),
                UserProfileId = Guid.NewGuid(),
                Rating = 4.5f,
                Comment = "Excellent service!"
            };
        }



        public void AssertPagedResult(PagedResult<ReviewViewModel> result, int expectedPageNumber, int expectedPageSize)
        {
            result.Should().NotBeNull();
            result.PageNumber.Should().Be(expectedPageNumber);
            result.PageSize.Should().Be(expectedPageSize);
        }

        public void AssertReview(ReviewViewModel viewModel, Guid expectedId)
        {
            viewModel.Should().NotBeNull();
            viewModel.Id.Should().Be(expectedId);
        }

        public void AssertCreatedReview(ReviewViewModel viewModel, CreateReviewViewModel createModel)
        {
            viewModel.Should().NotBeNull();
            viewModel.UserProfileId.Should().Be(createModel.UserProfileId);
            viewModel.AssistanceId.Should().Be(createModel.AssistanceId);
            viewModel.Rating.Should().Be(createModel.Rating);
            viewModel.Comment.Should().Be(createModel.Comment);
        }

        public void AssertReviewDeleted(Guid reviewId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var review = dbContext.Reviews.FirstOrDefault(r => r.Id == reviewId);
                review.Should().BeNull();
            }
        }
    }
}
