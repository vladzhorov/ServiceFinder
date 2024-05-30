using FluentValidation;
using ServiceFinder.API.ViewModels.Assistance;

namespace ServiceFinder.API.Validators.Assistance
{
    public class CreateAssistanceViewModelValidator : AbstractValidator<CreateAssistanceViewModel>
    {
        public CreateAssistanceViewModelValidator()
        {
            RuleFor(model => model.UserProfileId).NotEmpty();
            RuleFor(model => model.AssistanceCategoryId).NotEmpty();
            RuleFor(model => model.Title).NotEmpty().MaximumLength(100);
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Duration).GreaterThan(TimeSpan.Zero);
            RuleFor(model => model.Location).NotEmpty();
        }
    }
}
