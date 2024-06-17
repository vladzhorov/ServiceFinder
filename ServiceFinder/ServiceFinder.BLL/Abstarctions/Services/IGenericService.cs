using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.BLL.Abstractions.Services
{
    public interface IGenericService<TModel>
    {
        Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken);
        Task<TModel> UpdateAsync(Guid id, TModel model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResult<TModel>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}

