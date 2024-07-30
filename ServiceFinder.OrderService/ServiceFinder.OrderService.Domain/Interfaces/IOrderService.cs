using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.Shared.Enums;

namespace ServiceFinder.OrderService.Domain.Interfaces
{
    public interface IOrderService
    {
        Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, string email, CancellationToken cancellationToken);
        Task CreateOrderAsync(Order order, decimal baseRatePerMinute, int baseRateDurationInMinutes, CancellationToken cancellationToken);
    }
}