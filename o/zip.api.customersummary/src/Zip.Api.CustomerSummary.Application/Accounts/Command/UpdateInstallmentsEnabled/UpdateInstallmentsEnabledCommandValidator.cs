using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.UpdateInstallmentsEnabled
{
    public class UpdateInstallmentsEnabledCommandValidator : AbstractValidator<UpdateInstallmentsEnabledCommand>
    {
        public UpdateInstallmentsEnabledCommandValidator()
        {
            RuleFor(x => x.AccountId)
               .GreaterThan(0)
               .WithMessage("Invalid AccountId");

            RuleFor(x => x.AccountTypeId)
               .GreaterThan(0)
               .WithMessage("Invalid AccountTypeId");

            RuleFor(x => x.IsInstallmentsEnabled)
               .NotNull()
               .WithMessage("Missing IsInstallmentsEnabled");
        }
    }
}
