using FluentValidation;
using ServiceFinder.API.Constants;
using ServiceFinder.API.ViewModels.Review;

namespace ServiceFinder.API.Validators.Review
{
    public class CreateReviewViewModelValidator : AbstractValidator<CreateReviewViewModel>
    {
        public CreateReviewViewModelValidator()
        {
            RuleFor(model => model.AssistanceId).NotEmpty();
            RuleFor(model => model.Rating).InclusiveBetween(ValidationConstants.MinimumRating, ValidationConstants.MaximumRating);
            RuleFor(model => model.Comment).NotEmpty();
        }
    }
}