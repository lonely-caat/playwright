using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface ISmsContentContext
    {
        Task<SmsContent> GetAsync(int id);

        Task<SmsContent> GetAsync(string name);
    }
}
