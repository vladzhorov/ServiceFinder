using ServiceFinder.BLL.Abstractions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;

namespace ServiceFinder.BLL.Abstarctions.Services
{
    public interface IReviewService : IGenericService<ReviewEntity, Review>
    {
    }
}
