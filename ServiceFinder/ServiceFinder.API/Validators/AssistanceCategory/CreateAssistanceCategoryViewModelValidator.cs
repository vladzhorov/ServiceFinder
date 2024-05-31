using FluentValidation;
using ServiceFinder.API.DI;
using ServiceFinder.API.ViewModels.AssistanceCategory;

namespace ServiceFinder.API.Validators.AssistanceCategory
{
    public class CreateAssistanceCategoryViewModelValidator : AbstractValidator<CreateAssistanceCategoryViewModel>
    {
        public CreateAssistanceCategoryViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().MaximumLength(ConstraintValues.MaximumNameLength);
            RuleFor(model => model.Description).NotEmpty();
        }
    }
}
