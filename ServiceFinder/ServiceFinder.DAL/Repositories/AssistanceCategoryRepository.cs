using Microsoft.EntityFrameworkCore;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.PaginationObjects;

namespace ServiceFinder.DAL.Repositories
{
    public class AssistanceCategoryRepository : Repository<AssistanceCategoryEntity>, IAssistanceCategoryRepository
    {
        public AssistanceCategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<AssistanceCategoryEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Query
                 .Include(ac => ac.Assistances)
                 .ThenInclude(a => a.Reviews)
                 .Include(ac => ac.Assistances)
                 .ThenInclude(a => a.UserProfile)
                 .FirstOrDefaultAsync(ac => ac.Id == id, cancellationToken);
        }

        public override async Task<PagedResult<AssistanceCategoryEntity>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = Query.Include(ac => ac.Assistances).ThenInclude(a => a.Reviews).Include(ac => ac.Assistances).ThenInclude(a => a.UserProfile);
            return await GetPagedResultAsync(query, pageNumber, pageSize, cancellationToken);
        }

        public async override Task<AssistanceCategoryEntity> UpdateAsync(AssistanceCategoryEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
