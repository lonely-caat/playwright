using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models
{
    public class OrdersResponse
    {
        public IList<OrderDetailResponse> Orders { get; set; }
    }
}
