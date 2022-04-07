using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.PayNow.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.PayNow
{
    public interface IPayNowLinkServiceProxy
    {
        [Post("/api/paynowlink/consumer")]
        Task<PayNowLinkResponse> PayNowLinkAsync(PayNowLinkRequest request);

        [Get("/health")]
        Task<string> HealthCheck();
    }
}
