using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm.Models;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
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
            var result = customerId == int.MaxValue
                ? null
                : new Pagination<CommentDto>() { Items = new List<CommentDto>() { new CommentDto() {ReferenceId = customerId} } };
            return Task.FromResult(result);
        }

    }
}
