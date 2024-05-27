using AutoMapper;
using ServiceFinder.BLL.Abstractions.Services;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.BLL.Services
{
    public class GenericService<TEntity, TModel> : IGenericService<TEntity, TModel> where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        public GenericService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async virtual Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, CancellationToken.None);
            var model = _mapper.Map<TModel>(entity);
            return model;
        }

        public async virtual Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _repository.AddAsync(entity, cancellationToken);
            return _mapper.Map<TModel>(result);
        }

        public async virtual Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _repository.UpdateAsync(entity, cancellationToken);
            return _mapper.Map<TModel>(result);
        }

        public async virtual Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            await _repository.DeleteAsync(entity, cancellationToken);
        }

        public async virtual Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            var models = _mapper.Map<List<TModel>>(entities);
            return models;
        }
    }
}
