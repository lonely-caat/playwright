using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces
{
    public interface IAccountSearchServiceClient
    {
        Task<IEnumerable<AccountListItem>> SearchAccountsAsync(
            CustomerSearchType searchType,
            string searchValue,
            int skip,
            int take);

        Task GetStatusAsync();
    }
}