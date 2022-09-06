using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount
{
    public class LockAccountCommandValidator : AbstractValidator<LockAccountCommand>
    {
        public LockAccountCommandValidator()
        {
            RuleFor(x => x.ConsumerId)
                .GreaterThan(0)
                .WithMessage("Invalid consumer id.");

            RuleFor(x => x.AccountId)
                .GreaterThan(0)
                .WithMessage("Invalid account id.");
        }
    }
}
