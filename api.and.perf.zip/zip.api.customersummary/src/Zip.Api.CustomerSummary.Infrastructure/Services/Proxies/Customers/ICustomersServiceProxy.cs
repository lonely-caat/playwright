using Refit;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers
{
    public interface ICustomersServiceProxy
    {
        [Put("/customer-api/v1/customers/{customerId}/email/validate")]
        Task<ValidateCustomerEmailResponse> ValidateCustomerEmail(string customerId, ValidateCustomerEmailRequest request);

        [Put("/customer-api/v1/customers/{customerId}/email")]
        Task<UpdateCustomerResponse> UpdateCustomerEmail(string customerId, UpdateCustomerEmailRequest request);

        [Put("/customer-api/v1/customers/{customerId}/mobile/validate")]
        Task<ValidateCustomerMobileResponse> ValidateCustomerMobile(string customerId, ValidateCustomerMobileRequest request);

        [Put("/customer-api/v1/customers/{customerId}/mobile")]
        Task<UpdateCustomerMobileResponse> UpdateCustomerMobile(string customerId, UpdateCustomerMobileRequest request);

        [Get("/customer-api/diagnostics/ping")]
        Task<string> Ping();
    }
}
