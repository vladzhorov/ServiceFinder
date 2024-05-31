using FluentValidation;
using ServiceFinder.API.DI;
using ServiceFinder.API.ViewModels.Review;

namespace ServiceFinder.API.Validators.Review
{
    public class CreateReviewViewModelValidator : AbstractValidator<CreateReviewViewModel>
    {
        public CreateReviewViewModelValidator()
        {
            RuleFor(model => model.AssistanceId).NotEmpty();
            RuleFor(model => model.Rating).InclusiveBetween(ConstraintValues.MinimumRating, ConstraintValues.MaximumRating);
            RuleFor(model => model.Comment).NotEmpty();
        }
    }
}