using MediatR;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Requests;

namespace Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderSummary
{
    public class GetOrderSummaryQuery : OrderSummaryRequest, IRequest<GetOrderSummaryResponse>
    {
    }
}
