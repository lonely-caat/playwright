using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.SetDefaultPaymentMethod
{
    public class SetDefaultPaymentMethodCommandValidator : AbstractValidator<SetDefaultPaymentMethodCommand>
    {
        public SetDefaultPaymentMethodCommandValidator()
        {
            RuleFor(x => x.ConsumerId)
                .GreaterThan(0)
                .WithMessage("Invalid consumer id.");

            RuleFor(x => x.PaymentMethodId)
                .NotEmpty()
                .WithMessage("Invalid payment method id.");
        }
    }
}
