using AutoFixture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
{
    public class MockTransactionHistoryContext : ITransactionHistoryContext
    {
        private readonly IFixture _fixture;

        public MockTransactionHistoryContext()
        {
            _fixture = new Fixture();
        }

        public Task<IEnumerable<TransactionHistory>> FindByConsumerIdAsync(long consumerId, DateTime startDate, DateTime endDate)
        {
            return Task.FromResult<IEnumerable<TransactionHistory>>(new List<TransactionHistory>
            {
                _fixture.Create<TransactionHistory>()
            });
        }

        public Task<IEnumerable<TransactionHistory>> FindByAccountIdAsync(long accountId, DateTime startDateTime, DateTime endDateTime)
        {
            return Task.FromResult<IEnumerable<TransactionHistory>>(new List<TransactionHistory>
            {
                _fixture.Create<TransactionHistory>()
            });
        }
    }
}
