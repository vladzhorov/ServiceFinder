using ServiceFinder.BLL.Models;

namespace ServiceFinder.BLL.Abstarctions.Services
{
    public interface IReviewable
    {
        public delegate ICollection<Review> ReviewsGetter<T>(T item);
    }
}
