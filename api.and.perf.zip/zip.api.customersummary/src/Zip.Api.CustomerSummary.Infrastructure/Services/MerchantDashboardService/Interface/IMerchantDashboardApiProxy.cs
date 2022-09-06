using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Requests;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Interface
{
    public interface IMerchantDashboardApiProxy
    {
        [Get("/health")]
        Task<string> HealthCheck();


        [Get("/api/v1/order/summary")]
        Task<HttpResponseMessage> SendGetOrderSummaryRequestAsync([Query] OrderSummaryRequest request, CancellationToken cancellationToken);
    }
}
