using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Domain.Interfaces
{
    public interface IOrderService
    {
        Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken);
        Task CreateOrderAsync(Order order, decimal baseRatePerMinute, int baseRateDurationInMinutes, CancellationToken cancellationToken);
    }
}