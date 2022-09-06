using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.Transactions.Query.GetVcnTransactions
{
    public class GetVcnTransactionsQueryValidator : AbstractValidator<GetVcnTransactionsQuery>
    {
        public GetVcnTransactionsQueryValidator()
        {
            RuleFor(x => x.NetworkReferenceId)
                    .NotEmpty()
                    .WithMessage("Empty networkReferenceId");
        }
    }
}
