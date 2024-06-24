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

namespace ServiceFinder.UnitTests
{
    public class ReviewServiceTests
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IAssistanceRepository _assistanceRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;
        private readonly ReviewService _service;
        private readonly Fixture _fixture;

        public ReviewServiceTests()
        {
            _reviewRepository = Substitute.For<IReviewRepository>();
            _assistanceRepository = Substitute.For<IAssistanceRepository>();
            _userProfileRepository = Substitute.For<IUserProfileRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            });
            _mapper = config.CreateMapper();
            _service = new ReviewService(_reviewRepository, _assistanceRepository, _userProfileRepository, _mapper);

            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task CreateAsync_CorrectModel_ReturnsCreatedModel()
        {
            // Arrange
            var model = _fixture.Create<Review>();
            var entity = _mapper.Map<ReviewEntity>(model);

            _assistanceRepository.GetByIdAsync(model.AssistanceId, Arg.Any<CancellationToken>())
                .Returns(new AssistanceEntity());
            _userProfileRepository.GetByIdAsync(model.UserProfileId, Arg.Any<CancellationToken>())
                .Returns(new UserProfileEntity());

            _reviewRepository.AddAsync(Arg.Any<ReviewEntity>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(entity));

            // Act
            var result = await _service.CreateAsync(model, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(model);
        }

        [Fact]
        public async Task UpdateAsync_CorrectModel_ReturnsUpdatedModel()
        {
            // Arrange
            var entity = _fixture.Create<ReviewEntity>();
            var model = _mapper.Map<Review>(entity);

            _reviewRepository.GetByIdAsync(entity.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(entity));
            _reviewRepository.UpdateAsync(Arg.Any<ReviewEntity>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(entity));

            // Act
            var result = await _service.UpdateAsync(entity.Id, model, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(model);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsModel()
        {
            // Arrange
            var entity = _fixture.Create<ReviewEntity>();
            var model = _mapper.Map<Review>(entity);

            _reviewRepository.GetByIdAsync(entity.Id, Arg.Any<CancellationToken>())
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

            _reviewRepository.GetByIdAsync(id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<ReviewEntity>(null));

            // Act
            Func<Task> act = async () => await _service.GetByIdAsync(id, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsPagedResult()
        {
            // Arrange
            var entities = _fixture.CreateMany<ReviewEntity>(10).ToList();
            var pagedEntities = new PagedResult<ReviewEntity>
            {
                Data = entities,
                PageNumber = 1,
                PageSize = 10,
                TotalCount = 10,
                TotalPages = 1
            };
            var pagedModels = new PagedResult<Review>
            {
                Data = _mapper.Map<List<Review>>(entities),
                PageNumber = 1,
                PageSize = 10,
                TotalCount = 10,
                TotalPages = 1
            };

            _reviewRepository.GetAllAsync(1, 10, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(pagedEntities));

            // Act
            var result = await _service.GetAllAsync(1, 10, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(pagedModels);
        }

        [Fact]
        public async Task DeleteAsync_ExistingId_DeletesModel()
        {
            // Arrange
            var entity = _fixture.Create<ReviewEntity>();

            _reviewRepository.GetByIdAsync(entity.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(entity));
            _reviewRepository.DeleteAsync(entity, Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(entity.Id, CancellationToken.None);

            // Assert
            await _reviewRepository.Received(1).DeleteAsync(entity, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ThrowsModelNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();

            _reviewRepository.GetByIdAsync(id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<ReviewEntity>(null));

            // Act
            Func<Task> act = async () => await _service.DeleteAsync(id, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ModelNotFoundException>();
        }
    }
}
