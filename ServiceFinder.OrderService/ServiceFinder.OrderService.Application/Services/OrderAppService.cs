using AutoMapper;
using ServiceFinder.Domain.PaginationObjects;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Services;

public class OrderAppService : IOrderAppService
{
    private readonly OrderService _orderService;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderAppService(OrderService orderService, IOrderRepository orderRepository, IMapper mapper)
    {
        _orderService = orderService;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDTO, decimal baseRatePerMinute, int baseRateDurationInMinutes, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(orderDTO);
        await _orderService.CreateOrderAsync(order, baseRatePerMinute, baseRateDurationInMinutes, cancellationToken);
        return _mapper.Map<OrderDto>(order);
    }

    public Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken)
    {
        return _orderService.UpdateOrderStatusAsync(orderId, newStatus, cancellationToken);
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<OrderDto>(order);
    }
    public Task<PagedResult<OrderDto>> GetAllOrderAsync(int pageNumber, int pageSize)
    {
        var pagedEntitiesTask = _orderRepository.GetAllAsync(pageNumber, pageSize);

        return pagedEntitiesTask.ContinueWith(task =>
        {
            var pagedEntities = task.Result;

            var mappedResult = _mapper.Map<PagedResult<OrderDto>>(pagedEntities);
            return mappedResult;
        });
    }
}
