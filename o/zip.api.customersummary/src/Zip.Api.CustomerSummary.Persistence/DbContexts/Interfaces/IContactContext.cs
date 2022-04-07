using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IContactContext
    {
        Task<string> GetMobileAsync(long consumerId);

        Task<ContactDto> GetContactAsync(long consumerId);
    }
}