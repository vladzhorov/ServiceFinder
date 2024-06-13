using Microsoft.EntityFrameworkCore;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.DAL.Repositories
{
    public class AssistanceRepository : Repository<AssistanceEntity>, IAssistanceRepository
    {
        public AssistanceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<AssistanceEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Query
                .Include(a => a.UserProfile)
                .Include(a => a.AssistanceCategory)
                .Include(a => a.Reviews)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public override async Task<List<AssistanceEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Query
                .Include(a => a.UserProfile)
                .Include(a => a.AssistanceCategory)
                .Include(a => a.Reviews)
                .ToListAsync(cancellationToken);
        }

        public async override Task<AssistanceEntity> AddAsync(AssistanceEntity entity, CancellationToken cancellationToken)
        {
            await Query.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return await Query
                .Include(a => a.UserProfile)
                .Include(a => a.AssistanceCategory)
                .Include(a => a.Reviews)
                .FirstOrDefaultAsync(a => a.Id == entity.Id, cancellationToken) ?? entity;
        }
        public async override Task<AssistanceEntity> UpdateAsync(AssistanceEntity entity, CancellationToken cancellationToken)
        {
            var existingEntity = await Query.AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == entity.Id, cancellationToken);

            entity.UserProfileId = existingEntity?.UserProfileId ?? entity.UserProfileId;
            entity.AssistanceCategoryId = existingEntity?.AssistanceCategoryId ?? entity.AssistanceCategoryId;
            entity.CreatedAt = existingEntity.CreatedAt;

            Query.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}