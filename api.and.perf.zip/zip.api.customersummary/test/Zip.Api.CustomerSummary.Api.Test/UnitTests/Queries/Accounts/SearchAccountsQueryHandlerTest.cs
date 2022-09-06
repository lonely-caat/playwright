using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.SearchAccounts;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Accounts
{
    public class SearchAccountsQueryHandlerTest
    {
        private readonly Mock<IAccountSearchServiceClient> _accountSearchServiceClient;

        public SearchAccountsQueryHandlerTest()
        {
            _accountSearchServiceClient = new Mock<IAccountSearchServiceClient>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SearchAccountsQueryHandler(null));
        }

        [Fact]
        public async Task Should_return()
        {
            _accountSearchServiceClient.Setup(x => x.SearchAccountsAsync(It.IsAny<CustomerSearchType>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<AccountListItem>
                {
                    new AccountListItem()
                    {
                        Id = 2
                    },
                    new AccountListItem()
                    {
                        Id=3
                    }
                });

            var handler = new SearchAccountsQueryHandler(_accountSearchServiceClient.Object);
            var result = await handler.Handle(new SearchAccountsQuery(), CancellationToken.None);
            Assert.Equal(2, result.Count());
        }
    }
}
