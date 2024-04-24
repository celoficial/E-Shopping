using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserName is required")
                .NotNull()
                .MaximumLength(70)
                .WithMessage("UserName must not exceed 70 characteres");
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("EmailAddress is required")
                .NotNull()
                .EmailAddress();
            RuleFor(x => x.TotalPrice)
                .NotEmpty()
                .WithMessage("TotalPrice is required")
                .GreaterThan(-1)
                .WithMessage("TotalPrice should be greater than -1");
        }
    }
}
