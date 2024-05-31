using FluentValidation;
using ServiceFinder.API.DI;
using ServiceFinder.API.ViewModels.AssistanceCategory;

namespace ServiceFinder.API.Validators.AssistanceCategory
{
    public class UpdateAssistanceCategoryViewModelValidator : AbstractValidator<UpdateAssistanceCategoryViewModel>
    {
        public UpdateAssistanceCategoryViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().MaximumLength(ConstraintValues.MaximumNameLength);
            RuleFor(model => model.Description).NotEmpty();
        }
    }
}
