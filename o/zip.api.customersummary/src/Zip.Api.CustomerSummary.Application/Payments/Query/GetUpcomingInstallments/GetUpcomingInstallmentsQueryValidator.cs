using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetUpcomingInstallments
{
    public class GetUpcomingInstallmentsQueryValidator : AbstractValidator<GetUpcomingInstallmentsQuery>
    {
        public GetUpcomingInstallmentsQueryValidator()
        {
            RuleFor(x => x.AccountId)
               .GreaterThan(0)
               .WithMessage("Invalid AccountId");

            RuleFor(x => x.CustomerId)
               .NotEmpty()
               .WithMessage("Missing CustomerId");

            RuleFor(x => x.ToDate)
               .NotEmpty()
               .WithMessage("Missing ToDate");
        }
    }
}
