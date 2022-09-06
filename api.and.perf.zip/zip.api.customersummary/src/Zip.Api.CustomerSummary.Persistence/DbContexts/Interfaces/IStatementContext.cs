using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Statement;
using Zip.Api.CustomerSummary.Domain.Entities.Statements;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IStatementContext
    {
        Task<IEnumerable<Statement>> GetAsync(long consumerId);

        Task<Statement> GetAsync(long consumerId, DateTime statementDate);

        Task<Statement> GetAsync(string statementName);

        Task CreateAsync(Statement statement);

        Task UpdateAsync(Statement statement);

        Task<IEnumerable<StatementDate>> GetStatementDatesAsync(long accountId);
    }
}
