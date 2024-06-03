using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ServiceFinder.API.Constants;
using ServiceFinder.API.ViewModels.Review;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Models;

namespace ServiceFinder.API.Controller
{
    [ApiController]
    [Route(ApiRoutes.Reviews)]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateReviewViewModel> _createReviewViewModelValidator;

        public ReviewController(IValidator<CreateReviewViewModel> createReviewViewModelValidator,
            IMapper mapper,
            IReviewService reviewService)
        {
            _createReviewViewModelValidator = createReviewViewModelValidator;
            _mapper = mapper;
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<List<ReviewViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ReviewViewModel>>(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ReviewViewModel> GetById(Guid id, CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<ReviewViewModel>(review);
        }

        [HttpPost]
        public async Task<ReviewViewModel> Create(CreateReviewViewModel viewModel, CancellationToken cancellationToken)
        {
            await _createReviewViewModelValidator.ValidateAndThrowAsync(viewModel, cancellationToken);
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
