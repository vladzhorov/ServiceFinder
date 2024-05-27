using AutoMapper;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interfaces;

namespace ServiceFinder.BLL.Services
{
    public class ReviewService : GenericService<ReviewEntity, Review>, IReviewService
    {
        public ReviewService(IReviewRepository reviewRepository, IMapper mapper) : base(reviewRepository, mapper)
        {
        }
    }
}

