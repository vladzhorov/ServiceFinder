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
            var query = Query.Include(r => r.Assistance).Include(r => r.UserProfile);
            return await GetPagedResultAsync(query, pageNumber, pageSize, cancellationToken);
        }

        public async override Task<ReviewEntity> AddAsync(ReviewEntity entity, CancellationToken cancellationToken)
        {
            var review = await base.AddAsync(entity, cancellationToken);
            _dbContext.Entry(review).State = EntityState.Detached;
            return review;
        }

        public override async Task DeleteAsync(ReviewEntity entity, CancellationToken cancellationToken)
        {
            var existingReview = await Query.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == entity.Id, cancellationToken);

            if (existingReview != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync(cancellationToken);
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
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
