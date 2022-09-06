using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.PayNow
{
    public class PayNowCommandValidator : AbstractValidator<PayNowCommand>
    {
        public PayNowCommandValidator()
        {
            RuleFor(x => x.ConsumerId)
                .GreaterThan(0)
                .WithMessage("Invalid consumer id.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Invalid amount.");

            RuleFor(x => x.OriginatorEmail)
                .NotEmpty();

            RuleFor(x => x.OriginatorIpAddress)
                .NotEmpty();
        }
    }
}
