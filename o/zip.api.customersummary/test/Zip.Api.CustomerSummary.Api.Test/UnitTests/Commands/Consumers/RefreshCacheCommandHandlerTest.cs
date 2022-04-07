using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.RefreshCache;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Invalidation;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class RefreshCacheCommandHandlerTest
    {
        private readonly RefreshCacheCommandHandler _target;
        private readonly Mock<IConsumerContext> _consumerContext;
        private readonly Mock<IAccountsService> _accountsService;
        public RefreshCacheCommandHandlerTest()
        {
            _consumerContext = new Mock<IConsumerContext>();
            _accountsService = new Mock<IAccountsService>();
            _target = new RefreshCacheCommandHandler(_accountsService.Object, _consumerContext.Object);
        }

        [Fact]
        public void Given_AnyNullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RefreshCacheCommandHandler(_accountsService.Object, null));
        }

        [Fact]
        public async Task Given_ConsumerAccountNotFound_ShouldThrow_AccountNotFoundException()
        {
            _consumerContext.Setup(x => x.GetAccountInfoAsync(It.IsAny<long>())).ReturnsAsync(null as AccountInfo);
            
            await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
            {
                await _target.Handle(new RefreshCacheCommand(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_Consumer_ShouldSendInvalidationRequest_ForConsumerAccountId()
        {
            _consumerContext.Setup(x => x.GetAccountInfoAsync(It.IsAny<long>())).ReturnsAsync(new AccountInfo() {AccountId = 2});

            _accountsService.Setup(x => x.Invalidate(It.IsAny<InvalidationRequest>())).ReturnsAsync(new InvalidationResponse());

            await _target.Handle(new RefreshCacheCommand(), CancellationToken.None);

            _accountsService.Verify(x => x.Invalidate(It.Is<InvalidationRequest>(r => r.AccountIds.Count == 1 && r.AccountIds.Contains("2"))), Times.Once);
        }
    }
}
