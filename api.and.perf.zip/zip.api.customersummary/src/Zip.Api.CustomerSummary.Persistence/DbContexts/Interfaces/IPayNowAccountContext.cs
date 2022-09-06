using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.PayNowUrlGenerator;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IPayNowAccountContext
    {
        Task<PayNowLinkAccount> GetPayNowLinkAccountAsync(long accountId);
    }
}