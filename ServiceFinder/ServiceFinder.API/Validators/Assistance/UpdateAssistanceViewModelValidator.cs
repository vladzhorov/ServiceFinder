using FluentValidation;
using ServiceFinder.API.DI;
using ServiceFinder.API.ViewModels.Assistance;

namespace ServiceFinder.API.Validators.Assistance
{
    public class UpdateAssistanceViewModelValidator : AbstractValidator<UpdateAssistanceViewModel>
    {
        public UpdateAssistanceViewModelValidator()
        {
            RuleFor(model => model.Title).NotEmpty().MaximumLength(ConstraintValues.MaximumTitleLength);
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(ConstraintValues.MinimumPrice);
            RuleFor(x => x.Duration).GreaterThan(TimeSpan.Zero);
            RuleFor(model => model.Location).NotEmpty();
        }
    }
}
