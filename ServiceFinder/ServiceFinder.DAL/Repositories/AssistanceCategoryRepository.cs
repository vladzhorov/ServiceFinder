using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.DAL.Repositories
{
    public class AssistanceCategoryRepository : Repository<AssistanceCategoryEntity>, IAssistanceCategoryRepository
    {
        public AssistanceCategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
