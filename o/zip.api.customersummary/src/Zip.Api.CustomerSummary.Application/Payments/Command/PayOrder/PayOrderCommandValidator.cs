using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.PayOrder
{
    public class PayOrderCommandValidator : AbstractValidator<PayOrderCommand>
    {
        public PayOrderCommandValidator()
        {
            RuleFor(x => x.AccountId)
               .GreaterThan(0)
               .WithMessage("Invalid AccountId");

            RuleFor(x => x.CustomerId)
               .NotEmpty()
               .WithMessage("Invalid CustomerId");

            RuleFor(x => x.OrderId)
               .GreaterThan(0)
               .WithMessage("Invalid OrderId");

            RuleFor(x => x.Amount)
               .GreaterThan(0)
               .WithMessage("Invalid Amount");
        }
    }
}