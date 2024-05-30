using FluentValidation;
using ServiceFinder.API.ViewModels.AssistanceCategory;

namespace ServiceFinder.API.Validators.AssistanceCategory
{
    public class CreateAssistanceCategoryViewModelValidator : AbstractValidator<CreateAssistanceCategoryViewModel>
    {
        public CreateAssistanceCategoryViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().MaximumLength(50);
            RuleFor(model => model.Description).NotEmpty();
        }
    }
}
