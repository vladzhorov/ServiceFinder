using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.ViewModels.Review;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _ReviewService;
        private readonly IMapper _mapper;

        public ReviewController(IMapper mapper, IReviewService ReviewService)
        {
            _mapper = mapper;
            _ReviewService = ReviewService;
        }

        [HttpGet]
        public async Task<List<ReviewViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var Reviews = await _ReviewService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ReviewViewModel>>(Reviews);
        }

        [HttpGet("{id}")]
        public async Task<ReviewViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var Review = await _ReviewService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<ReviewViewModel>(Review);
        }

        [HttpPost]
        public async Task<ReviewViewModel> Create(CreateReviewViewModel viewModel, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(viewModel);
            var result = await _ReviewService.CreateAsync(review, cancellationToken);
            return _mapper.Map<ReviewViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _ReviewService.DeleteAsync(id, cancellationToken);
        }
    }
}
