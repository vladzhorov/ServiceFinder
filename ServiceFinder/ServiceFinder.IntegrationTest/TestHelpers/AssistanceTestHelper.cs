using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.API.ViewModels.Assistance;
using ServiceFinder.DAL;
using ServiceFinder.DAL.PaginationObjects;


namespace ServiceFinder.IntegrationTests.TestHelpers
{
    public class AssistanceTestHelper
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AssistanceTestHelper(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void AssertPagedResult(PagedResult<AssistanceViewModel> result, int pageNumber, int pageSize)
        {
            result.Should().NotBeNull();
            result.Data.Should().NotBeNullOrEmpty();
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
        }

        public void AssertAssistance(AssistanceViewModel result, Guid id)
        {
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        public void AssertCreatedAssistance(AssistanceViewModel result, CreateAssistanceViewModel viewModel)
        {
            result.Should().NotBeNull();
            result.Title.Should().Be(viewModel.Title);
            result.Description.Should().Be(viewModel.Description);
            result.Price.Should().Be(viewModel.Price);
            result.DurationInMinutes.Should().Be(viewModel.DurationInMinutes);
            result.Location.Should().Be(viewModel.Location);
            result.UserProfileId.Should().Be(viewModel.UserProfileId);
            result.AssistanceCategoryId.Should().Be(viewModel.AssistanceCategoryId);
        }

        public void AssertUpdatedAssistance(AssistanceViewModel result, Guid id, UpdateAssistanceViewModel viewModel)
        {
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Title.Should().Be(viewModel.Title);
            result.Description.Should().Be(viewModel.Description);
            result.Price.Should().Be(viewModel.Price);
            result.DurationInMinutes.Should().Be(viewModel.DurationInMinutes);
            result.Location.Should().Be(viewModel.Location);
        }

        public void AssertAssistanceDeleted(Guid id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var deletedAssistance = dbContext.Assistances.Find(id);
                deletedAssistance.Should().BeNull();
            }
        }
    }
}
