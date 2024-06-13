using Microsoft.EntityFrameworkCore;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using System.Linq.Expressions;

namespace ServiceFinder.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;
        protected DbSet<T> Query => _dbContext.Set<T>();
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual Task<List<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return Query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
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

        public async virtual Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Query.Where(predicate).ToListAsync(cancellationToken);
        }
    }
}
