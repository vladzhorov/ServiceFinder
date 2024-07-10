using Microsoft.EntityFrameworkCore;
using ServiceFinder.Domain.PaginationObjects;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using System.Linq.Expressions;

namespace ServiceFinder.OrderService.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly OrderDbContext _dbContext;
        protected DbSet<T> Query => _dbContext.Set<T>();
        public Repository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected PagedResult<T> GetPagedResultAsync(IQueryable<T> query, int pageNumber, int pageSize)
        {
            int totalCount = query.Count();
            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = data
            };
        }

        public virtual PagedResult<T> GetAll(int pageNumber, int pageSize)
        {
            IQueryable<T> query = Query;
            return GetPagedResultAsync(query, pageNumber, pageSize);
        }

        public virtual Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Query.FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken).AsTask();
        }


        public async virtual Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await Query.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async virtual Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            Query.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async virtual Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            Query.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            return Query.Where(predicate).ToList();
        }
    }
}
