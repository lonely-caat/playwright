using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.AddAttributeAndLockAccount
{
    public class AddAttributeAndLockAccountValidator : AbstractValidator<AddAttributeAndLockAccountCommand>
    {
        public AddAttributeAndLockAccountValidator()
        {
            RuleFor(x => x.ConsumerId)
                .GreaterThan(0)
                .WithMessage("Invalid consumer id.");

            RuleFor(x => x.AccountId)
                .GreaterThan(0)
                .WithMessage("Invalid account id.");

            RuleFor(x => x.Reason)
                .NotEmpty();

            RuleFor(x => x.Attribute)
                .NotEmpty();
        }
    }
}
