using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IAccountTypeContext
    {
        Task<AccountType> GetAsync(long id);
    }
}