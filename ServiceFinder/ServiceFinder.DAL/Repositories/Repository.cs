using Microsoft.EntityFrameworkCore;
using ServiceFinder.DAL.Interfaces;
using System.Linq.Expressions;

namespace ServiceFinder.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public virtual Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _dbContext.Set<T>().FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken).AsTask();
        }


        public async virtual Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async virtual Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async virtual Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async virtual Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync(cancellationToken);
        }
    }
}
