using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;

namespace Zip.Api.CustomerSummary.Application.Transactions.Query.GetLmsTransactions
{
    public class GetLmsTransactionsQuery : IRequest<IEnumerable<LmsTransaction>>
    {
        public long AccountId { get; set; }
    }
}
