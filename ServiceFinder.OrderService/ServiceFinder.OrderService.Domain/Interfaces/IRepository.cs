using ServiceFinder.Domain.PaginationModels;
using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResult<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);

    }
}