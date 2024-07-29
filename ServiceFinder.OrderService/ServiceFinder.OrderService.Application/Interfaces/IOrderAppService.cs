using ServiceFinder.Domain.PaginationModels;
using ServiceFinder.Shared.Enums;

namespace ServiceFinder.OrderService.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDTO, decimal baseRatePerMinute, int baseRateDurationInMinutes, CancellationToken cancellationToken);
        Task UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, string email, CancellationToken cancellationToken);
        Task<OrderDto> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResult<OrderDto>> GetAllOrderAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}