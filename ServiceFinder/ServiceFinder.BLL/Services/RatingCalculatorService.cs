using static ServiceFinder.BLL.Abstarctions.Services.IReviewable;

namespace ServiceFinder.BLL.Services
{
    public class RatingCalculatorService
    {
        public static float CalculateRating<T>(T item, ReviewsGetter<T> reviewsGetter)
        {
            var reviews = reviewsGetter(item);

            if (reviews == null || !reviews.Any())
                return 0;

            return reviews.Average(r => r.Rating);
        }
    }
}