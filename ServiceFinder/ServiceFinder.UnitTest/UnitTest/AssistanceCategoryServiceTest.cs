using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Exceptions;
using ServiceFinder.BLL.Mapper;
using ServiceFinder.BLL.Models;
using ServiceFinder.BLL.Services;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.UnitTest.UnitTest
{
    public class AssistanceCategoryServiceTests
    {
        private readonly IFixture _fixture;
        private readonly IAssistanceCategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAssistanceCategoryService _service;

        public AssistanceCategoryServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _repository = Substitute.For<IAssistanceCategoryRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            });
            _mapper = config.CreateMapper();

            _service = new AssistanceCategoryService(_repository, _mapper);
        }

        [Fact]
        public async Task CreateAsync_CorrectModel_ReturnsCreatedModel()
        {
            // Arrange
            var model = _fixture.Create<AssistanceCategory>();
            var entity = _mapper.Map<AssistanceCategoryEntity>(model);

            _repository.AddAsync(Arg.Any<AssistanceCategoryEntity>(), default)
                .Returns(entity);

            // Act
            var result = await _service.CreateAsync(model, default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(model, options => options
                .Excluding(x => x.Assistances));
        }

        [Fact]
        public async Task GetAllAsync_Paged_ReturnsPagedResultOfAssistanceCategoryModels()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            var pagedEntities = _fixture.Create<PagedResult<AssistanceCategoryEntity>>();
            var pagedModels = _mapper.Map<PagedResult<AssistanceCategory>>(pagedEntities);

            _repository.GetAllAsync(pageNumber, pageSize, default)
                .Returns(pagedEntities);

            // Act
            var result = await _service.GetAllAsync(pageNumber, pageSize, default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(pagedModels);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsCorrectModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = _fixture.Build<AssistanceCategoryEntity>().With(e => e.Id, id).Create();
            _repository.GetByIdAsync(id, default).Returns(entity);

            // Act
            var result = await _service.GetByIdAsync(id, default);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ThrowsModelNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repository.GetByIdAsync(id, default).Returns((AssistanceCategoryEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ModelNotFoundException>(() => _service.GetByIdAsync(id, default));
        }

        [Fact]
        public async Task UpdateAsync_ExistingId_ReturnsUpdatedModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var model = _fixture.Create<AssistanceCategory>();
            var entity = _mapper.Map<AssistanceCategoryEntity>(model);
            entity.Id = id;
            _repository.UpdateAsync(Arg.Any<AssistanceCategoryEntity>(), default).Returns(entity);
            _repository.GetByIdAsync(id, default).Returns(entity);

            // Act
            var result = await _service.UpdateAsync(id, model, default);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingId_ThrowsModelNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var model = _fixture.Create<AssistanceCategory>();

            _repository.GetByIdAsync(id, default).Returns((AssistanceCategoryEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ModelNotFoundException>(() => _service.UpdateAsync(id, model, default));
        }

        [Fact]
        public async Task DeleteAsync_ExistingId_DeletesSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = _fixture.Build<AssistanceCategoryEntity>().With(e => e.Id, id).Create();
            _repository.GetByIdAsync(id, default).Returns(entity);

            // Act
            await _service.DeleteAsync(id, default);

            // Assert
            await _repository.Received(1).DeleteAsync(entity, default);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ThrowsModelNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repository.GetByIdAsync(id, default).Returns((AssistanceCategoryEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ModelNotFoundException>(() => _service.DeleteAsync(id, default));
        }
    }
}
