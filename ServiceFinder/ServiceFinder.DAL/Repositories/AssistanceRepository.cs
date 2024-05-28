using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.DAL.Repositories
{
    public class AssistanceRepository : Repository<AssistanceEntity>, IAssistanceRepository
    {
        public AssistanceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
