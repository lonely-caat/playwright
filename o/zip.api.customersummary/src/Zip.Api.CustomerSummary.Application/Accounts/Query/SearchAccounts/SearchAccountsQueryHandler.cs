using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.SearchAccounts
{
    public class SearchAccountsQueryHandler : IRequestHandler<SearchAccountsQuery, IEnumerable<AccountListItem>>
    {
        private readonly IAccountSearchServiceClient _client;

        public SearchAccountsQueryHandler(IAccountSearchServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<AccountListItem>> Handle(SearchAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _client.SearchAccountsAsync(CustomerSearchType.NoFilter, request.SearchValue, 0, 100);
        }
    }
}
