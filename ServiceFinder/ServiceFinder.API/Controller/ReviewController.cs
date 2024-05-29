using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.DI;
using ServiceFinder.API.ViewModels.Review;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route(ApiRoutes.reviews)]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IMapper mapper, IReviewService ReviewService)
        {
            _mapper = mapper;
            _reviewService = ReviewService;
        }

        [HttpGet]
        public async Task<List<ReviewViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var Reviews = await _reviewService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ReviewViewModel>>(Reviews);
        }

        [HttpGet("{id}")]
        public async Task<ReviewViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var Review = await _reviewService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<ReviewViewModel>(Review);
        }

        [HttpPost]
        public async Task<ReviewViewModel> Create(CreateReviewViewModel viewModel, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(viewModel);
            var result = await _reviewService.CreateAsync(review, cancellationToken);
            return _mapper.Map<ReviewViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _reviewService.DeleteAsync(id, cancellationToken);
        }
    }
}
