using Microsoft.EntityFrameworkCore;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

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

        public override async Task<List<AssistanceCategoryEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Query
                .Include(ac => ac.Assistances)
                .ToListAsync(cancellationToken);
        }

    }
}
