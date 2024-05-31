using FluentValidation;
using ServiceFinder.API.Constants;
using ServiceFinder.API.ViewModels.UserProfile;

namespace ServiceFinder.API.Validators.UserProfile
{
    public class CreateUserProfileViewModelValidator : AbstractValidator<CreateUserProfileViewModel>
    {
        public CreateUserProfileViewModelValidator()
        {
            RuleFor(model => model.PhotoURL).NotEmpty();
            RuleFor(model => model.PhoneNumber).NotEmpty().Matches(ValidationConstants.PhoneNumberPattern);
        }
    }
}
