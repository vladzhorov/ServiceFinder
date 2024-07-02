using ServiceFinder.Domain.PaginationObjects;
using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Domain.Interfaces
{
    public interface IOrderRequestRepository
    {
        Task<OrderRequest> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResult<OrderRequest>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task AddAsync(OrderRequest orderRequest, CancellationToken cancellationToken);
        Task UpdateAsync(OrderRequest orderRequest, CancellationToken cancellationToken);
    }
}
