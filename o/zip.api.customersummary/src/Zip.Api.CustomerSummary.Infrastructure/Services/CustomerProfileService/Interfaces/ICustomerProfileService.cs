using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CustomerProfileService.Interfaces
{
    public interface ICustomerProfileService
    {
        Task<ConsumerPersonalInfo> GetConsumerPersonalInfo(string searchKey);
    }
}