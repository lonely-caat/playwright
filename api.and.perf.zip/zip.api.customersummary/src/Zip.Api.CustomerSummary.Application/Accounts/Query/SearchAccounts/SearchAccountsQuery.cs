using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.SearchAccounts
{
    public class SearchAccountsQuery : IRequest<IEnumerable<AccountListItem>>
    {
        public string SearchValue { get; set; }

        public SearchAccountsQuery(string searchValue)
        {
            SearchValue = searchValue;
        }

        public SearchAccountsQuery()
        {

        }
    }
}
