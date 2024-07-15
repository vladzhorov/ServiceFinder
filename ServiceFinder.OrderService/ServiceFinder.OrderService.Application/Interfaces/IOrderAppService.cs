using ServiceFinder.OrderService.Application.DTOs;
using ServiceFinder.OrderService.Domain.Enums;

namespace ServiceFinder.OrderService.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDTO, decimal baseRatePerMinute, int baseRateDurationInMinutes, CancellationToken cancellationToken);
        Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken);
        Task<OrderDto> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}