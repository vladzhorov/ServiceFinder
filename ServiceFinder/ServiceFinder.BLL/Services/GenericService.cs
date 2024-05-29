using AutoMapper;
using ServiceFinder.BLL.Abstractions.Services;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.BLL.Services
{
    public class GenericService<TEntity, TModel> : IGenericService<TModel> where TEntity : class
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
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            var model = _mapper.Map<TModel>(entity);
            return model;
        }

        public async virtual Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _repository.AddAsync(entity, cancellationToken);
            return _mapper.Map<TModel>(result);
        }

        public async virtual Task<TModel> UpdateAsync(Guid id, TModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(model);
            var propertyEntity = entity.GetType().GetProperty("Id");
            if (propertyEntity == null)
            {
                throw new InvalidOperationException($"The entity type {entity.GetType().Name} does not have a property named 'Id'.");
            }
            propertyEntity.SetValue(entity, id);
            var result = await _repository.UpdateAsync(entity, cancellationToken);
            return _mapper.Map<TModel>(result);
        }

        public async virtual Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
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
