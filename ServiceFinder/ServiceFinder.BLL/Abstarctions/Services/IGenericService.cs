namespace ServiceFinder.BLL.Abstractions.Services
{
    public interface IGenericService<TModel>
    {
        Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken);
        Task<TModel> UpdateAsync(Guid id, TModel model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken);

    }
}

