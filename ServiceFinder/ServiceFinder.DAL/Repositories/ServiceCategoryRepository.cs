using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.DAL.Repositories
{
    public class ServiceCategoryRepository : Repository<ServiceCategoryEntity>, IServiceCategoryRepository
    {
        public ServiceCategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
