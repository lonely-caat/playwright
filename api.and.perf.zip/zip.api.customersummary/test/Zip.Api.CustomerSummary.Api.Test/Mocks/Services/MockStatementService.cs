using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockStatementService : IStatementsService
    {
        public Task<bool> GenerateStatementsAsync(GenerateStatementsRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
