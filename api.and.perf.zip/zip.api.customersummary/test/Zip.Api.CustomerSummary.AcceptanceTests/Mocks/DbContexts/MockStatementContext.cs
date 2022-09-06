using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Statement;
using Zip.Api.CustomerSummary.Domain.Entities.Statements;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
{
    public class MockStatementContext : IStatementContext
    {
        public Task CreateAsync(Statement statement)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Statement>> GetAsync(long consumerId)
        {
            throw new NotImplementedException();
        }

        public Task<Statement> GetAsync(long consumerId, DateTime statementDate)
        {
            throw new NotImplementedException();
        }

        public Task<Statement> GetAsync(string statementName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StatementDate>> GetStatementDatesAsync(long accountId)
        {
            return Task.FromResult<IEnumerable<StatementDate>>(new List<StatementDate>
            {
                new StatementDate
                {
                    EndDate = new DateTime(2019,10,1),
                    StartDate = new DateTime(2019,9,1)
                },
                new StatementDate
                {
                    EndDate = new DateTime(2019,11,1),
                    StartDate = new DateTime(2019,10,1)
                },
            });
        }

        public Task UpdateAsync(Statement statement)
        {
            throw new NotImplementedException();
        }
    }
}
