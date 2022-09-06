using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Requests;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Responses;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Interface
{
    public interface IMerchantDashboardService
    {
        Task<OrderSummaryResponse> GetOrderSummaryAsync(
            OrderSummaryRequest request,
            CancellationToken cancellationToken);
    }
}
