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
                .FirstOrDefaultAsync(ac => ac.Id == id, cancellationToken);
        }

        public override async Task<PagedResult<AssistanceCategoryEntity>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<AssistanceCategoryEntity> query = Query.Include(ac => ac.Assistances);
            return await GetPagedResultAsync(query, pageNumber, pageSize, cancellationToken);
        }
    }
}
