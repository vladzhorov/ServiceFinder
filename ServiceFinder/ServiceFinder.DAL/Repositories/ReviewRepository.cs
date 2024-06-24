using Microsoft.EntityFrameworkCore;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.DAL.Repositories
{
    public class ReviewRepository : Repository<ReviewEntity>, IReviewRepository
    {
        public ReviewRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<PagedResult<ReviewEntity>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<ReviewEntity> query = Query.Include(r => r.Assistance).Include(r => r.UserProfile);
            return await GetPagedResultAsync(query, pageNumber, pageSize, cancellationToken);
        }

        public async override Task<ReviewEntity> AddAsync(ReviewEntity entity, CancellationToken cancellationToken)
        {
            await Query.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return await Query
                .Include(r => r.Assistance)
                .Include(r => r.UserProfile)
                .FirstOrDefaultAsync(r => r.Id == entity.Id, cancellationToken) ?? entity;
        }

        public async override Task<ReviewEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Query
                .Include(r => r.Assistance)
                .Include(r => r.UserProfile)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }
    }
}