using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm.Models;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockCrmServiceProxy : ICrmServiceProxy
    {
        public Task<string> HealthCheck()
        {
            return Task.FromResult(JsonConvert.SerializeObject(new HealthCheckResult(HealthStatus.Healthy)));
        }

        public Task<CommentDto> CreateComment(CreateCommentRequest request)
        {
            return Task.FromResult(new CommentDto());
        }

        public Task<Pagination<CommentDto>> GetCustomerComment(long customerId, long pageIndex, long pageSize)
        {
            return Task.FromResult(new Pagination<CommentDto>());
        }

    }
}
