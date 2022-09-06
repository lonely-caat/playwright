using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService.Interfaces
{
    public interface IIdentityService
    {
        Task<UserDetail> GetUserByEmailAsync(string email);
    }
}
