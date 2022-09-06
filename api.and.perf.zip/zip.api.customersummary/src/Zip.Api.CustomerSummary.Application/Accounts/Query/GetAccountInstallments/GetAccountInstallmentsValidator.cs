using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInstallments
{
    public class GetAccountInstallmentsValidator : AbstractValidator<GetAccountInstallmentsQuery>
    {
        public GetAccountInstallmentsValidator()
        {
            RuleFor(x => x.AccountId)
               .GreaterThan(0)
               .WithMessage("Invalid AccountId");
        }
    }
}
