using FluentValidation;
using ServiceFinder.API.Constants;
using ServiceFinder.OrderService.API.ViewModels.Order;

namespace ServiceFinder.API.Validators
{
    public class CreateOrderViewModelValidator : AbstractValidator<CreateOrderViewModel>
    {
        public CreateOrderViewModelValidator()
        {
            RuleFor(x => x.ServiceId).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ScheduledDate).NotEmpty();
            RuleFor(x => x.DurationInMinutes).NotEmpty();
            RuleFor(x => x.BaseRatePerMinute).GreaterThan(ValidationConstants.BaseRatePerMinuteMinValue);
            RuleFor(x => x.BaseRateDurationInMinutes).GreaterThan(ValidationConstants.BaseRateDurationInMinutesMinValue);
        }
    }
}
