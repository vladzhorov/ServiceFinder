using Microsoft.EntityFrameworkCore;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

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
                .ThenInclude(a => a.Reviews)
                .Include(up => up.Reviews)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public override async Task<List<UserProfileEntity>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await Query
                .Include(u => u.Assistances)
                 .ThenInclude(a => a.Reviews)
                .Include(u => u.Reviews)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
