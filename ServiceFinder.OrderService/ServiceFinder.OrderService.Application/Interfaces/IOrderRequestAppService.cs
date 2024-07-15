using ServiceFinder.Domain.PaginationModels;
using ServiceFinder.OrderService.Application.DTOs;
using ServiceFinder.OrderService.Domain.Enums;

namespace ServiceFinder.OrderService.Application.Interfaces
{
    public interface IOrderRequestAppService
    {
        Task<OrderRequestDto> CreateOrderRequestAsync(OrderRequestDto orderRequestDTO, CancellationToken cancellationToken);
        Task UpdateOrderRequestStatusAsync(Guid orderRequestId, OrderRequestStatus newStatus, CancellationToken cancellationToken);
        Task<OrderRequestDto> GetOrderRequestByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResult<OrderRequestDto>> GetAllOrderRequestAsync(int pageNumber, int pageSize);
    }
}
