using System;
using FluentValidation;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCardTransactions
{
    public class GetCardTransactionsQueryValidator : AbstractValidator<GetCardTransactionsQuery>
    {
            public GetCardTransactionsQueryValidator()
            { 
                    RuleFor(x => x.CardId)
                            .NotEmpty()
                            .Must(y => y != Guid.Empty)
                            .WithMessage("Invalid Card Id");
            }
    }
}
