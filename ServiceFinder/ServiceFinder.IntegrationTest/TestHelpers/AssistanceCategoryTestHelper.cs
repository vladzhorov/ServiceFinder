using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.API.ViewModels.AssistanceCategory;
using ServiceFinder.DAL;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.PaginationObjects;
using ServiceFinder.IntegrationTest;

namespace ServiceFinder.IntegrationTests.TestHelpers
{
    public class AssistanceCategoryTestHelper
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Program> _factory;

        public AssistanceCategoryTestHelper(TestingWebAppFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            AddTestData();
        }

        private void AddTestData()
        {
            var categoriesToAdd = new List<AssistanceCategoryEntity>
            {
                new AssistanceCategoryEntity { Id = Guid.NewGuid(), Name = "Category 1", Description = "Category 1 Description" },
                new AssistanceCategoryEntity { Id = Guid.NewGuid(), Name = "Category 2", Description = "Category 2 Description" }
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.AssistanceCategories.AddRange(categoriesToAdd);
                dbContext.SaveChanges();
            }
        }

        public AssistanceCategoryViewModel GetFirstCategory()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var category = dbContext.AssistanceCategories.FirstOrDefault();
                category.Should().NotBeNull();
                return new AssistanceCategoryViewModel
                {
                    Id = category!.Id,
                    Name = category.Name,
                    Description = category.Description
                };
            }
        }

        public static CreateAssistanceCategoryViewModel CreateAssistanceCategoryViewModel()
        {
            return new CreateAssistanceCategoryViewModel
            {
                Name = "New Category",
                Description = "New Category Description"
            };
        }

        public static UpdateAssistanceCategoryViewModel UpdateAssistanceCategoryViewModel()
        {
            return new UpdateAssistanceCategoryViewModel
            {
                Name = "Updated Category Name",
                Description = "Updated Category Description"
            };
        }

        public void AssertPagedResult(PagedResult<AssistanceCategoryViewModel> result, int pageNumber, int pageSize)
        {
            result.Should().NotBeNull();
            result.Data.Should().NotBeNullOrEmpty();
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
        }

        public void AssertAssistanceCategory(AssistanceCategoryViewModel result, Guid id)
        {
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        public void AssertCreatedAssistanceCategory(AssistanceCategoryViewModel result, CreateAssistanceCategoryViewModel viewModel)
        {
            result.Should().NotBeNull();
            result.Name.Should().Be(viewModel.Name);
            result.Description.Should().Be(viewModel.Description);
        }

        public void AssertUpdatedAssistanceCategory(AssistanceCategoryViewModel result, Guid id, UpdateAssistanceCategoryViewModel viewModel)
        {
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Name.Should().Be(viewModel.Name);
            result.Description.Should().Be(viewModel.Description);
        }

        public void AssertCategoryDeleted(Guid id)
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var deletedCategory = dbContext.AssistanceCategories.Find(id);
                deletedCategory.Should().BeNull();
            }
        }
    }
}
