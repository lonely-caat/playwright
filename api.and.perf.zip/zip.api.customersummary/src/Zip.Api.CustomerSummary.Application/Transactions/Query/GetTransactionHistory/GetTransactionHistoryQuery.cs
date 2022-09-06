using System;
using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;

namespace Zip.Api.CustomerSummary.Application.Transactions.Query.GetTransactionHistory
{
    public class GetTransactionHistoryQuery : IRequest<IEnumerable<TransactionHistory>>
    {
        public long ConsumerId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public GetTransactionHistoryQuery(long consumerId, DateTime? startDate, DateTime? endDate)
        {
            ConsumerId = consumerId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public GetTransactionHistoryQuery()
        {
        }
    }
}
