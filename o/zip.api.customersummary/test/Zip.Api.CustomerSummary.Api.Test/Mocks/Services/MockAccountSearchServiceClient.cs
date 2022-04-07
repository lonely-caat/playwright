using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockAccountSearchServiceClient : IAccountSearchServiceClient
    {
        private readonly List<AccountListItem> _items;

        public MockAccountSearchServiceClient()
        {
            _items = new List<AccountListItem>
            {
                new AccountListItem
                {
                    FirstName = "Michael",
                    LastName = "Jordan"
                },
                new AccountListItem
                {
                    FirstName = "Micky",
                    LastName = "Mouse"
                }
            };
        }
        public Task GetStatusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountListItem>> SearchAccountsAsync(CustomerSearchType searchType, string searchValue, int skip, int take)
        {
            if(searchValue == "throw")
                throw new Exception("test exception");
            return Task.FromResult<IEnumerable<AccountListItem>>(_items);
        }
    }
}
