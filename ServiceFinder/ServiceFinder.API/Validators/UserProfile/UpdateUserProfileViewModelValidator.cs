using FluentValidation;
using ServiceFinder.API.ViewModels.UserProfile;

namespace ServiceFinder.API.Validators.UserProfile
{
    public class UpdateUserProfileViewModelValidator : AbstractValidator<UpdateUserProfileViewModel>
    {
        public UpdateUserProfileViewModelValidator()
        {
            RuleFor(model => model.PhotoURL).NotEmpty();
            RuleFor(model => model.PhoneNumber).NotEmpty().Matches(@"8\(0(29|44|33|25)\)\d{3}-\d{2}-\d{2}");
        }
    }
}
