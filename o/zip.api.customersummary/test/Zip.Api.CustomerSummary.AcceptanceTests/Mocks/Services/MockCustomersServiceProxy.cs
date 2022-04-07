using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers.Models;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockCustomersServiceProxy : ICustomersServiceProxy
    {

        public Task<string> Ping()
        {
            return Task.FromResult(JsonConvert.SerializeObject(new HealthCheckResult(HealthStatus.Healthy)));
        }

        public Task<UpdateCustomerResponse> UpdateCustomerEmail(string customerId, UpdateCustomerEmailRequest request)
        {
            return Task.FromResult(new UpdateCustomerResponse
            {
                Success = true
            });
        }

        public Task<UpdateCustomerMobileResponse> UpdateCustomerMobile(string customerId, UpdateCustomerMobileRequest request)
        {
            return Task.FromResult(new UpdateCustomerMobileResponse
            {
                Success = true
            });
        }

        public Task<ValidateCustomerEmailResponse> ValidateCustomerEmail(string customerId, ValidateCustomerEmailRequest request)
        {
            return Task.FromResult(new ValidateCustomerEmailResponse
            {
                Success = true
            });
        }

        public Task<ValidateCustomerMobileResponse> ValidateCustomerMobile(string customerId, ValidateCustomerMobileRequest request)
        {
            return Task.FromResult(new ValidateCustomerMobileResponse
            {

                Success = true
            });
        }
    }
}
