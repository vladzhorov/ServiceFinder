using FluentValidation;
using ServiceFinder.API.Constants;
using ServiceFinder.API.ViewModels.Assistance;

namespace ServiceFinder.API.Validators.Assistance
{
    public class CreateAssistanceViewModelValidator : AbstractValidator<CreateAssistanceViewModel>
    {
        public CreateAssistanceViewModelValidator()
        {
            RuleFor(model => model.UserProfileId).NotEmpty();
            RuleFor(model => model.AssistanceCategoryId).NotEmpty();
            RuleFor(model => model.Title).NotEmpty().MaximumLength(ValidationConstants.MaximumTitleLength);
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(ValidationConstants.MinimumPrice);
            RuleFor(x => x.DurationInMinutes).GreaterThan(0);
            RuleFor(model => model.Location).NotEmpty();
        }
    }
}
