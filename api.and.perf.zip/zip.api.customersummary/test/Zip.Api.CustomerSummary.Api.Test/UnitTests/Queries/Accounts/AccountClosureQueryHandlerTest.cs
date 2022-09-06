using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountClosure;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Accounts
{
    public class AccountClosureQueryHandlerTest
    {
        private readonly Mock<IAccountsService> _accountsService = new Mock<IAccountsService>();

        [Fact]
        public async Task Given_NullAccount_ShouldReturn_GoodForClose()
        {
            _accountsService.Setup(x => x.GetAccount(It.IsAny<string>()))
                .ReturnsAsync(null as AccountResponse);
            var handler = new GetAccountClosureQueryHandler(_accountsService.Object);
            var result = await handler.Handle(new GetAccountClosureQuery(), CancellationToken.None);
            Assert.Equal(AccountClosureEnquireResult.GoodForClose, result);
        }

        [Fact]
        public async Task Given_AccountNotNullBalanceGreaterThan0_ShouldReturn_PendingTransactions()
        {
            _accountsService.Setup(x => x.GetAccount(It.IsAny<string>()))
                .ReturnsAsync(new AccountResponse() { 
                    Balance = 291.2m
                });
            var handler = new GetAccountClosureQueryHandler(_accountsService.Object);
            var result = await handler.Handle(new GetAccountClosureQuery(), CancellationToken.None);
            Assert.Equal(AccountClosureEnquireResult.PendingTransactions, result);
        }

        [Fact]
        public async Task Given_AccountPendingBalanceGreaterThan0_ShouldReturn_PendingTransactions()
        {
            _accountsService.Setup(x => x.GetAccount(It.IsAny<string>()))
                .ReturnsAsync(new AccountResponse()
                {
                    PendingBalance = 291.2m
                });
            var handler = new GetAccountClosureQueryHandler(_accountsService.Object);
            var result = await handler.Handle(new GetAccountClosureQuery(), CancellationToken.None);
            Assert.Equal(AccountClosureEnquireResult.PendingTransactions, result);
        }


        [Fact]
        public async Task Given_AvailableBalanceLessThanCreditLimit_ShouldReturn_TransactionsHaveFutureClearingDays()
        {
            _accountsService.Setup(x => x.GetAccount(It.IsAny<string>()))
                .ReturnsAsync(new AccountResponse()
                {
                    AvailableBalance = 120,
                    Configuration = new AccountResponseConfiguration()
                    {
                        CreditLimit = 150
                    }
                });
            var handler = new GetAccountClosureQueryHandler(_accountsService.Object);
            var result = await handler.Handle(new GetAccountClosureQuery(), CancellationToken.None);
            Assert.Equal(AccountClosureEnquireResult.TransactionsHaveFutureClearingDays, result);
        }
    }
}
