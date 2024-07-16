using FluentValidation;
using ServiceFinder.API.ViewModels.OrderRequest;

namespace ServiceFinder.OrderService.API.Validators
{
    public class CreateOrderRequestViewModelValidator : AbstractValidator<CreateOrderRequestViewModel>
    {
        public CreateOrderRequestViewModelValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}