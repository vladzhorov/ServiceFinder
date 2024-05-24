using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.DAL.Repositories
{
    public class UserProfileRepository : Repository<UserProfileEntity>, IUserProfileRepository
    {
        public UserProfileRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
