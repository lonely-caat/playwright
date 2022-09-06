using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.PayOrder;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces
{
    public interface ICoreGraphService
    {
        public Task<CoreGraphServiceHealthCheckResponse> HealthCheckAsync();

        public Task<IEnumerable<Installment>> GetUpcomingInstallmentsAsync(long accountId, Guid customerId, DateTime toDate);

        public Task<PayOrderInnerResponse> PayOrderAsync(PayOrderInput input);
    }
}
