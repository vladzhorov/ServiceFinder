using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.DAL.Repositories
{
    public class ServiceRepository : Repository<ServiceEntity>, IServiceRepository
    {
        public ServiceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
