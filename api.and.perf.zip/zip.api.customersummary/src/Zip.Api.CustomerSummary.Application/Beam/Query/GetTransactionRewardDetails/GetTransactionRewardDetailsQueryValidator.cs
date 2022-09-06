using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.GetTransactionRewardDetails
{
    public class GetTransactionRewardDetailsQueryValidator : AbstractValidator<GetTransactionRewardDetailsQuery>
    {
        public GetTransactionRewardDetailsQueryValidator()
        {
            RuleFor(x => x.CustomerId)
               .NotEmpty()
               .WithMessage("Missing CustomerId");

            RuleFor(x => x.TransactionId)
               .NotEmpty()
               .WithMessage("Missing TransactionId")
               .GreaterThan(0)
               .WithMessage("Invalid TransactionId");
        }
    }
}
