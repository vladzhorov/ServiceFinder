using FluentValidation;
using ServiceFinder.API.Constants;
using ServiceFinder.API.ViewModels.AssistanceCategory;

namespace ServiceFinder.API.Validators.AssistanceCategory
{
    public class CreateAssistanceCategoryViewModelValidator : AbstractValidator<CreateAssistanceCategoryViewModel>
    {
        public CreateAssistanceCategoryViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().MaximumLength(ValidationConstants.MaximumNameLength);
            RuleFor(model => model.Description).NotEmpty();
        }
    }
}
