using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using ServiceFinder.Domain.PaginationModels;
using ServiceFinder.OrderService.Application;
using ServiceFinder.OrderService.Application.DTOs;
using ServiceFinder.OrderService.Application.Mapper;
using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Events;
using ServiceFinder.OrderService.Domain.Exceptions;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Providers;
using ServiceFinder.OrderService.Domain.Services;

public class OrderRequestAppServiceTests
{
    private readonly IFixture _fixture;
    private readonly IOrderRequestRepository _orderRequestRepository;
    private readonly IMapper _mapper;
    private readonly OrderRequestService _orderRequestService;
    private readonly OrderRequestAppService _orderRequestAppService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public OrderRequestAppServiceTests()
    {
        _fixture = new Fixture();

        _orderRequestRepository = Substitute.For<IOrderRequestRepository>();
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _domainEventDispatcher = Substitute.For<IDomainEventDispatcher>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderMappingProfile>();
        });
        _mapper = config.CreateMapper();

        _orderRequestService = Substitute.For<OrderRequestService>(_orderRequestRepository, _domainEventDispatcher, _dateTimeProvider);
        _orderRequestAppService = new OrderRequestAppService(_orderRequestService, _orderRequestRepository, _mapper);
    }

    [Fact]
    public async Task CreateOrderRequestAsync_CorrectModel_ReturnsCreatedModel()
    {
        // Arrange
        var orderRequestDto = _fixture.Create<OrderRequestDto>();
        var orderRequest = _mapper.Map<OrderRequest>(orderRequestDto);
        var now = DateTime.UtcNow;
        _dateTimeProvider.UtcNow.Returns(now);

        // Act
        var result = await _orderRequestAppService.CreateOrderRequestAsync(orderRequestDto, default);

        // Assert
        result.Should().NotBeNull();
        result.CreatedAt.Should().Be(now);
        result.UpdatedAt.Should().Be(now);
        result.Status.Should().Be(OrderRequestStatus.Pending);
    }

    [Fact]
    public async Task UpdateOrderRequestStatusAsync_ShouldCallOrderRequestService()
    {
        // Arrange
        var orderRequestDto = _fixture.Create<OrderRequestDto>();
        var orderRequest = _mapper.Map<OrderRequest>(orderRequestDto);
        var newStatus = _fixture.Create<OrderRequestStatus>();
        var domainEventDispatcher = Substitute.For<IDomainEventDispatcher>();

        var orderRequestService = new OrderRequestService(_orderRequestRepository, domainEventDispatcher, _dateTimeProvider);
        var orderRequestAppService = new OrderRequestAppService(orderRequestService, _orderRequestRepository, _mapper);

        _orderRequestRepository.GetByIdAsync(orderRequest.Id, default)
            .Returns(orderRequest);

        // Act
        await orderRequestAppService.UpdateOrderRequestStatusAsync(orderRequest.Id, newStatus, default);

        // Assert
        await _orderRequestService.Received().UpdateOrderRequestStatusAsync(orderRequest.Id, newStatus, default);
        domainEventDispatcher.Received(1).Dispatch(Arg.Is<OrderRequestStatusChangedEvent>(e => e.OrderRequestId == orderRequest.Id && e.NewStatus == newStatus));
    }

    [Fact]
    public async Task GetOrderRequestByIdAsync_ExistingId_ReturnsModel()
    {
        // Arrange
        var orderRequestEntity = _fixture.Create<OrderRequest>();
        var orderRequestDto = _mapper.Map<OrderRequestDto>(orderRequestEntity);

        _orderRequestRepository.GetByIdAsync(orderRequestEntity.Id, default)
            .Returns(orderRequestEntity);

        // Act
        var result = await _orderRequestAppService.GetOrderRequestByIdAsync(orderRequestEntity.Id, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(orderRequestDto);
    }

    [Fact]
    public async Task GetOrderRequestByIdAsync_NonExistingId_ThrowsModelNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        _orderRequestRepository.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((OrderRequest?)null));

        // Act
        Func<Task> act = async () => await _orderRequestAppService.GetOrderRequestByIdAsync(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ModelNotFoundException>();
    }

    [Fact]
    public async Task GetAllOrderRequestAsync_ReturnsPagedResult()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var pagedEntities = _fixture.Create<PagedResult<OrderRequest>>();

        _orderRequestRepository.GetAllAsync(pageNumber, pageSize, default)
            .Returns(pagedEntities);

        // Act
        var result = await _orderRequestAppService.GetAllOrderRequestAsync(pageNumber, pageSize, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_mapper.Map<PagedResult<OrderRequestDto>>(pagedEntities));
    }
}
