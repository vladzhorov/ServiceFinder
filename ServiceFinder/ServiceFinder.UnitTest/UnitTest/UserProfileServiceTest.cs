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

    public class UserProfileServiceTests
    {
        private readonly IFixture _fixture;
        private readonly IUserProfileRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserProfileService _service;

        public UserProfileServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _repository = Substitute.For<IUserProfileRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            });
            _mapper = config.CreateMapper();

            _service = new UserProfileService(_repository, _mapper);
        }

        [Fact]
        public async Task CreateAsync_CorrectModel_ReturnsCreatedModel()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var model = _fixture.Create<UserProfile>();
            var entity = _mapper.Map<UserProfileEntity>(model);

            _repository.AddAsync(Arg.Any<UserProfileEntity>(), cancellationToken)
                .Returns(Task.FromResult(entity));

            // Act
            var result = await _service.CreateAsync(model, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(model, options => options
                .Excluding(x => x.Id)
                .Excluding(x => x.Reviews)
                .Excluding(x => x.Assistances));
        }

        [Fact]
        public async Task GetAllAsync_Paged_ReturnsPagedResultOfUserProfileModels()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            int pageNumber = 1;
            int pageSize = 10;
            var pagedEntities = _fixture.Create<PagedResult<UserProfileEntity>>();
            var pagedModels = _mapper.Map<PagedResult<UserProfile>>(pagedEntities);

            _repository.GetAllAsync(pageNumber, pageSize, cancellationToken)
                .Returns(Task.FromResult(pagedEntities));

            // Act
            var result = await _service.GetAllAsync(pageNumber, pageSize, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(pagedModels);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsModel()
        {
            // Arrange
            var entity = _fixture.Create<UserProfileEntity>();
            var model = _mapper.Map<UserProfile>(entity);

            _repository.GetByIdAsync(entity.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(entity));

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
                .Returns(Task.FromResult((UserProfileEntity)null));

            // Act
            Func<Task> act = async () => await _service.GetByIdAsync(id, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }



        [Fact]
        public async Task UpdateAsync_CorrectModel_ReturnsUpdatedModel()
        {
            // Arrange
            var entity = _fixture.Create<UserProfileEntity>();
            var model = _mapper.Map<UserProfile>(entity);

            _repository.GetByIdAsync(entity.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(entity));
            _repository.UpdateAsync(Arg.Any<UserProfileEntity>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(entity));

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
            var model = _fixture.Create<UserProfile>();
            var id = Guid.NewGuid();

            _repository.GetByIdAsync(id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<UserProfileEntity>(null));

            // Act
            Func<Task> act = async () => await _service.UpdateAsync(id, model, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ExistingId_DeletesModel()
        {
            // Arrange
            var entity = _fixture.Create<UserProfileEntity>();

            _repository.GetByIdAsync(entity.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(entity));
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
                .Returns(Task.FromResult<UserProfileEntity>(null));

            // Act
            Func<Task> act = async () => await _service.DeleteAsync(id, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }
    }
}
