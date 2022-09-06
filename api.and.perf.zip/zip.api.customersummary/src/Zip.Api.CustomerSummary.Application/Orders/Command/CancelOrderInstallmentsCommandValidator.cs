using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Orders.Command
{
    public class CancelOrderInstallmentsCommandValidator : AbstractValidator<CancelOrderInstallmentsCommand>
    {
        public CancelOrderInstallmentsCommandValidator()
        {
            RuleFor(x => x.AccountId)
               .GreaterThan(0)
               .WithMessage("Invalid AccountId");

            RuleFor(x => x.OrderId)
               .GreaterThan(0)
               .WithMessage("Invalid OrderId");
        }
    }
}
