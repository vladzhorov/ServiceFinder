using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using ServiceFinder.Domain.PaginationModels;
using ServiceFinder.OrderService.Application.Mapper;
using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Exceptions;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Providers;
using ServiceFinder.OrderService.Domain.Services;

public class OrderAppServiceTests
{
    private readonly IFixture _fixture;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly OrderService _orderService;
    private readonly OrderAppService _orderAppService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OrderAppServiceTests()
    {
        _fixture = new Fixture();

        _orderRepository = Substitute.For<IOrderRepository>();
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderMappingProfile>();
        });
        _mapper = config.CreateMapper();

        _orderService = Substitute.For<OrderService>(_orderRepository, Substitute.For<IDomainEventDispatcher>(), _dateTimeProvider);
        _orderAppService = new OrderAppService(_orderService, _orderRepository, _mapper);
    }

    [Fact]
    public async Task CreateOrderAsync_CorrectModel_ReturnsCreatedModel()
    {
        // Arrange
        var orderDto = _fixture.Create<OrderDto>();
        var order = _mapper.Map<Order>(orderDto);
        var now = DateTime.UtcNow;
        _dateTimeProvider.UtcNow.Returns(now);

        var baseRatePerMinute = 10.0m;
        var baseRateDurationInMinutes = 60;
        var expectedPrice = Math.Round((baseRatePerMinute / baseRateDurationInMinutes) * order.DurationInMinutes, 2);

        // Act
        var result = await _orderAppService.CreateOrderAsync(orderDto, baseRatePerMinute, baseRateDurationInMinutes, default);

        // Assert
        result.Should().NotBeNull();
        result.Price.Should().Be(expectedPrice);
        result.CreatedAt.Should().Be(now);
        result.UpdatedAt.Should().Be(now);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_ShouldCallOrderService()
    {
        // Arrange
        var orderDto = _fixture.Create<OrderDto>();
        var order = _mapper.Map<Order>(orderDto);
        var newStatus = _fixture.Create<OrderStatus>();

        _orderRepository.GetByIdAsync(order.Id, default)
            .Returns(order);

        // Act
        await _orderAppService.UpdateOrderStatusAsync(order.Id, newStatus, default);

        // Assert
        await _orderService.Received().UpdateOrderStatusAsync(order.Id, newStatus, default);
    }

    [Fact]
    public async Task GetOrderByIdAsync_ExistingId_ReturnsModel()
    {
        // Arrange
        var orderEntity = _fixture.Create<Order>();
        var orderDto = _mapper.Map<OrderDto>(orderEntity);

        _orderRepository.GetByIdAsync(orderEntity.Id, default)
            .Returns(orderEntity);

        // Act
        var result = await _orderAppService.GetOrderByIdAsync(orderEntity.Id, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(orderDto);
    }

    [Fact]
    public async Task GetOrderByIdAsync_NonExistingId_ThrowsModelNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        _orderRepository.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult((Order?)null));

        // Act
        Func<Task> act = async () => await _orderAppService.GetOrderByIdAsync(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ModelNotFoundException>();
    }
    [Fact]
    public async Task GetAllAsync_Paged_ReturnsPagedResultOfUserProfileModels()
    {
        // Arrange
        int pageNumber = 1;
        int pageSize = 10;
        var pagedEntities = _fixture.Create<PagedResult<Order>>();
        var pagedModels = _mapper.Map<PagedResult<OrderDto>>(pagedEntities);

        _orderRepository.GetAllAsync(pageNumber, pageSize, default)
            .Returns(pagedEntities);

        // Act
        var result = await _orderAppService.GetAllOrderAsync(pageNumber, pageSize, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(pagedModels);
    }
}
