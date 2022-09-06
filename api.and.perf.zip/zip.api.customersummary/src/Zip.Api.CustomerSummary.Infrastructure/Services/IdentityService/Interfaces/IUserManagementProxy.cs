using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService.Interfaces
{
    public interface IUserManagementProxy
    {
        [Get("/api/user/email")]
        Task<UserDetail> GetUserByEmailAsync([Query]string email);

        [Get("/health")]
        Task<string> HealthCheck();
    }
}
