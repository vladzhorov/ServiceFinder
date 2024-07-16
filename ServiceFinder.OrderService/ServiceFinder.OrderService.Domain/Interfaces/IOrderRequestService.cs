using ServiceFinder.OrderService.Domain.Enums;
using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Domain.Interfaces
{
    public interface IOrderRequestService
    {
        Task UpdateOrderRequestStatusAsync(Guid orderRequestId, OrderRequestStatus newStatus, CancellationToken cancellationToken);
        Task CreateOrderRequestAsync(OrderRequest orderRequest, CancellationToken cancellationToken);
    }
}