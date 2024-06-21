using Microsoft.EntityFrameworkCore;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.DAL.Repositories
{
    public class UserProfileRepository : Repository<UserProfileEntity>, IUserProfileRepository
    {
        public UserProfileRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<UserProfileEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Query
                .Include(up => up.Assistances)
                .ThenInclude(a => a.AssistanceCategory)
                .Include(up => up.Assistances)
                .ThenInclude(a => a.Reviews)
                .Include(up => up.Reviews)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public override async Task<PagedResult<UserProfileEntity>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = Query.Include(u => u.Assistances).ThenInclude(a => a.Reviews).Include(u => u.Reviews);
            return await GetPagedResultAsync(query, pageNumber, pageSize, cancellationToken);
        }
        public async override Task<UserProfileEntity> UpdateAsync(UserProfileEntity entity, CancellationToken cancellationToken)
        {
            var existingEntity = await Query.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == entity.Id, cancellationToken);


            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
