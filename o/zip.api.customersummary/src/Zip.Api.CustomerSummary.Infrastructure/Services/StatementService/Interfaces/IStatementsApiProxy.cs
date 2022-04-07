using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces
{
    public interface IStatementsApiProxy
    {
        [Get("/health")]
        Task<string> HealthCheck();
        
        [Post("/statements/trigger")]
        Task<HttpResponseMessage> TriggerStatementsGenerationAsync([Body] GenerateStatementsRequest request, CancellationToken cancellationToken);
    }
}
