using ServiceFinder.Domain.PaginationObjects;
using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        PagedResult<T> GetAll(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);

    }
}