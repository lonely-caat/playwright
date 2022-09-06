using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface ITransactionHistoryContext
    {
        Task<IEnumerable<TransactionHistory>> FindByConsumerIdAsync(
            long consumerId,
            DateTime startDate,
            DateTime endDate);

        Task<IEnumerable<TransactionHistory>> FindByAccountIdAsync(
            long accountId,
            DateTime startDate,
            DateTime endDate);
    }
}