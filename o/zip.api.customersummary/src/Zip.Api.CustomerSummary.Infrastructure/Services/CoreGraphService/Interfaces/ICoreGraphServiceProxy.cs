using Refit;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.GetUpcomingInstallments;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.PayOrder;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces
{
    public interface ICoreGraphServiceProxy
    {
        [Get("/.well-known/apollo/server-health")]
        public Task<CoreGraphServiceHealthCheckResponse> HealthCheckAsync();

        [Post("/internal/graphql")]
        public Task<GetUpcomingInstallmentsResponse> GetUpcomingInstallmentsAsync([Body] GetUpcomingInstallmentsRequest request);

        [Post("/internal/graphql")]
        public Task<PayOrderResponse> PayOrderAsync(
            [Header("x-zip-forwarded-for")] string ipAddress,
            [Body] PayOrderRequest request);
    }
}
