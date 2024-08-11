using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.Shared.Enums;

namespace ServiceFinder.OrderService.Domain.Interfaces
{
    public interface IOrderRequestService
    {
        Task UpdateOrderRequestStatusAsync(Guid orderRequestId, OrderRequestStatus newStatus, string email, CancellationToken cancellationToken);
        Task CreateOrderRequestAsync(OrderRequest orderRequest, CancellationToken cancellationToken);
    }
}