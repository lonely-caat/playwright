using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount
{
    public class CloseAccountCommandValidator : AbstractValidator<CloseAccountCommand>
    {
        public CloseAccountCommandValidator()
        {
            RuleFor(x => x.ConsumerId)
                .GreaterThan(0)
                .WithMessage("Invalid consumer id.");

            RuleFor(x => x.AccountId)
                .GreaterThan(0)
                .WithMessage("Invalid account id.");

            RuleFor(x => x.CreditProfileId)
                .GreaterThan(0);

            RuleFor(x => x.CreditStateType)
                .NotNull();
        }
    }
}
