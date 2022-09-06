using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces
{
    public interface IStatementsService
    {
        Task<bool> GenerateStatementsAsync(GenerateStatementsRequest request, CancellationToken cancellationToken);
    }
}
