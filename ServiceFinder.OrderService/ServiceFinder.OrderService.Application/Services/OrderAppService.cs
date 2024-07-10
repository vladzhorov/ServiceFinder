using AutoMapper;
using ServiceFinder.OrderService.Application.DTOs;
using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Domain.Services;

public class OrderAppService
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

    public async Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, newStatus, cancellationToken);
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<OrderDto>(order);
    }
}