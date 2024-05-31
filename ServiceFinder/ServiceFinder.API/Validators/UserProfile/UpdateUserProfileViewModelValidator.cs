using FluentValidation;
using ServiceFinder.API.DI;
using ServiceFinder.API.ViewModels.UserProfile;

namespace ServiceFinder.API.Validators.UserProfile
{
    public class UpdateUserProfileViewModelValidator : AbstractValidator<UpdateUserProfileViewModel>
    {
        public UpdateUserProfileViewModelValidator()
        {
            RuleFor(model => model.PhotoURL).NotEmpty();
            RuleFor(model => model.PhoneNumber).NotEmpty().Matches(ConstraintValues.PhoneNumberPattern);
        }
    }
}
