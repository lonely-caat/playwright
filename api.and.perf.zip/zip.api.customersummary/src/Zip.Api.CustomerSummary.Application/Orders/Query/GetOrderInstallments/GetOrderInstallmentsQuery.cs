using MediatR;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models;

namespace Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderInstallments
{
    public class GetOrderInstallmentsQuery : IRequest<OrderDetailResponse>
    {
        public long AccountId { get; set; }

        public long OrderId { get; set; }
    }
}
