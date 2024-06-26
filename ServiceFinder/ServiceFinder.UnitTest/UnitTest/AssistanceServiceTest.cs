using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using ServiceFinder.BLL.Exceptions;
using ServiceFinder.BLL.Mapper;
using ServiceFinder.BLL.Models;
using ServiceFinder.BLL.Services;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.UnitTest.UnitTest
{
    public class AssistanceServiceTests
    {
        private readonly IFixture _fixture;
        private readonly IAssistanceRepository _repository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IAssistanceCategoryRepository _assistanceCategoryRepository;
        private readonly IMapper _mapper;
        private readonly AssistanceService _service;

        public AssistanceServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _repository = Substitute.For<IAssistanceRepository>();
            _userProfileRepository = Substitute.For<IUserProfileRepository>();
            _assistanceCategoryRepository = Substitute.For<IAssistanceCategoryRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            });
            _mapper = config.CreateMapper();

            _service = new AssistanceService(_repository, _userProfileRepository, _assistanceCategoryRepository, _mapper);
        }

        [Fact]
        public async Task CreateAsync_CorrectModel_ReturnsCreatedModel()
        {
            // Arrange
            var model = _fixture.Create<Assistance>();
            var entity = _mapper.Map<AssistanceEntity>(model);

            _userProfileRepository.GetByIdAsync(model.UserProfileId, Arg.Any<CancellationToken>())
                .Returns(new UserProfileEntity());
            _assistanceCategoryRepository.GetByIdAsync(model.AssistanceCategoryId, Arg.Any<CancellationToken>())
                .Returns(new AssistanceCategoryEntity());

            _repository.AddAsync(Arg.Any<AssistanceEntity>(), Arg.Any<CancellationToken>())
                .Returns(entity);

            // Act
            var result = await _service.CreateAsync(model, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(model);
        }

        [Fact]
        public async Task CreateAsync_UserProfileDoesNotExist_ThrowsModelNotFoundException()
        {
            // Arrange
            var model = _fixture.Create<Assistance>();

            _userProfileRepository.GetByIdAsync(model.UserProfileId, Arg.Any<CancellationToken>())
                .Returns((UserProfileEntity?)null);

            // Act
            Func<Task> act = async () => await _service.CreateAsync(model, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Fact]
        public async Task CreateAsync_AssistanceCategoryDoesNotExist_ThrowsModelNotFoundException()
        {
            // Arrange
            var model = _fixture.Create<Assistance>();

            _userProfileRepository.GetByIdAsync(model.UserProfileId, Arg.Any<CancellationToken>())
                .Returns(new UserProfileEntity());
            _assistanceCategoryRepository.GetByIdAsync(model.AssistanceCategoryId, Arg.Any<CancellationToken>())
                .Returns((AssistanceCategoryEntity?)null);

            // Act
            Func<Task> act = async () => await _service.CreateAsync(model, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Fact]
        public async Task GetAllAsync_Paged_ReturnsPagedResultOfAssistanceModels()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            var pagedEntities = _fixture.Create<PagedResult<AssistanceEntity>>();
            var pagedModels = _mapper.Map<PagedResult<Assistance>>(pagedEntities);

            _repository.GetAllAsync(pageNumber, pageSize, default)
                .Returns(pagedEntities);

            // Act
            var result = await _service.GetAllAsync(pageNumber, pageSize, default);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(pagedModels);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsModel()
        {
            // Arrange
            var entity = _fixture.Create<AssistanceEntity>();
            var model = _mapper.Map<Assistance>(entity);

            _repository.GetByIdAsync(entity.Id, Arg.Any<CancellationToken>())
                .Returns(entity);

            // Act
            var result = await _service.GetByIdAsync(entity.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(model);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ThrowsModelNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repository.GetByIdAsync(id, Arg.Any<CancellationToken>())
                .Returns((AssistanceEntity?)null);

            // Act
            Func<Task> act = async () => await _service.GetByIdAsync(id, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Fact]
        public async Task UpdateAsync_CorrectModel_ReturnsUpdatedModel()
        {
            // Arrange
            var entity = _fixture.Create<AssistanceEntity>();
            var model = _mapper.Map<Assistance>(entity);

            _repository.GetByIdAsync(entity.Id, Arg.Any<CancellationToken>())
                .Returns(entity);
            _repository.UpdateAsync(Arg.Any<AssistanceEntity>(), Arg.Any<CancellationToken>())
                .Returns(entity);

            // Act
            var result = await _service.UpdateAsync(entity.Id, model, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(model);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingId_ThrowsModelNotFoundException()
        {
            // Arrange
            var model = _fixture.Create<Assistance>();
            var id = Guid.NewGuid();

            _repository.GetByIdAsync(id, Arg.Any<CancellationToken>())
                .Returns((AssistanceEntity?)null);

            // Act
            Func<Task> act = async () => await _service.UpdateAsync(id, model, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ExistingId_DeletesModel()
        {
            // Arrange
            var entity = _fixture.Create<AssistanceEntity>();

            _repository.GetByIdAsync(entity.Id, Arg.Any<CancellationToken>())
                .Returns(entity);
            _repository.DeleteAsync(entity, Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);

            // Act
            Func<Task> act = async () => await _service.DeleteAsync(entity.Id, CancellationToken.None);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ThrowsModelNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repository.GetByIdAsync(id, Arg.Any<CancellationToken>())
                .Returns((AssistanceEntity?)null);

            // Act
            Func<Task> act = async () => await _service.DeleteAsync(id, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }
    }
}
