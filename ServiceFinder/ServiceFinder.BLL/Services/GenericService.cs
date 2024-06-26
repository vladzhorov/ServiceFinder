﻿using AutoMapper;
using ServiceFinder.BLL.Abstractions.Services;
using ServiceFinder.BLL.Exceptions;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.BLL.Services
{
    public class GenericService<TEntity, TModel> : IGenericService<TModel> where TEntity : BaseEntity
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
            var entity = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new ModelNotFoundException(id);
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
            var checkIfEntityExists = await CheckIfEntityExists(id, cancellationToken);
            if (!checkIfEntityExists) throw new ModelNotFoundException(id);
            var entity = _mapper.Map<TEntity>(model);
            entity.Id = id;
            var result = await _repository.UpdateAsync(entity, cancellationToken);
            return _mapper.Map<TModel>(result);
        }

        public async virtual Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                throw new ModelNotFoundException(id);
            }
            await _repository.DeleteAsync(entity, cancellationToken);
        }

        public async virtual Task<PagedResult<TModel>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var pagedEntities = await _repository.GetAllAsync(pageNumber, pageSize, cancellationToken);
            return _mapper.Map<PagedResult<TModel>>(pagedEntities);
        }
        protected async Task<bool> CheckIfEntityExists(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity is null) return false;

            return true;
        }
    }
}
