using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces
{
    public interface ICustomerCoreService
    {
        Task<GetLoginStatusResponse> GetCustomerLoginStatusAsync(
            GetLoginStatusRequest request,
            CancellationToken cancellationToken);
        
        Task<UpdateLoginStatusResponse> DisableCustomerLoginAsync(
            UpdateLoginStatusRequest request,
            CancellationToken cancellationToken);
        
        Task<UpdateLoginStatusResponse> EnableCustomerLoginAsync(
            UpdateLoginStatusRequest request,
            CancellationToken cancellationToken);
    }
}