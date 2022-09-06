using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Transactions.Query.GetLmsTransactions;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Tango;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Transactions
{
    public class GetLmsTransactionsQueryHandlerTest
    {
        private readonly GetLmsTransactionsQueryHandler _target;

        private readonly Mock<IAccountsService> _accountsService;
        
        private readonly Mock<ITransactionHistoryContext> _transactionHistoryContext;

        private readonly Fixture _fixture;
    
        public GetLmsTransactionsQueryHandlerTest()
        {
            _accountsService = new Mock<IAccountsService>();
            _transactionHistoryContext = new Mock<ITransactionHistoryContext>();
            _fixture = new Fixture();

            _target = new GetLmsTransactionsQueryHandler(_accountsService.Object,
                                                         _transactionHistoryContext.Object);
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Action action = () => {
                new GetLmsTransactionsQueryHandler(null, null);
            };

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Given_NoLmsTransactionsFound_ShouldReturn_Null()
        {
            // Arrange
            _accountsService.Setup(x => x.GetTangoTransactions(
                                     It.IsAny<long>(),
                                     It.IsAny<DateTime>(),
                                     It.IsAny<DateTime>(),
                                     It.IsAny<bool>()))
                          .ReturnsAsync(null as IEnumerable<LoanMgtTransaction>);

            // Action
            var result = await _target.Handle(new GetLmsTransactionsQuery(),
                                              CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Given_LmsTransactionsFound_ShouldReturn_InOrder()
        {
            // Arrange
            var account = _fixture.Create<CustomerSummary.Domain.Entities.Accounts.Account>();
            var loanMgtTransaction = _fixture.Build<LoanMgtTransaction>()
                                             .With(x => x.ConsumerId, account.ConsumerId)
                                             .With(x => x.TransactionDate, DateTime.Today.ToShortDateString)
                                             .Create();
            var transactionHistory = _fixture.Build<TransactionHistory>()
                                             .With(x => x.AccountId, account.Id)
                                             .With(x => x.ConsumerId, account.ConsumerId)
                                             .With(x => x.ThreadId, loanMgtTransaction.ThreadID)
                                             .Create();

            _accountsService.Setup(x =>
                                     x.GetTangoTransactions(
                                         account.Id,
                                         It.IsAny<DateTime>(),
                                         It.IsAny<DateTime>(),
                                         It.IsAny<bool>()))
                          .ReturnsAsync(new List<LoanMgtTransaction> { loanMgtTransaction });

            _transactionHistoryContext.Setup(x =>
                                                 x.FindByAccountIdAsync(account.Id,
                                                                        It.IsAny<DateTime>(),
                                                                        It.IsAny<DateTime>()))
                                      .ReturnsAsync(new List<TransactionHistory> { transactionHistory });
            
            // Action
            var result = (await _target.Handle(
                new GetLmsTransactionsQuery { AccountId = account.Id },
                CancellationToken.None)).ToList();

            // Assert
            result.Count.Should().Be(1);
            result.Single().TransactionDate.Should().Be(DateTime.Today.Date);
            result.Single().Narrative.Should().Be(loanMgtTransaction.Narrative);
            result.Single().TransactionType.Should().Be(transactionHistory.Type.ToString());
            result.Single().Amount.Should().Be(loanMgtTransaction.TransactionAmount);
        }
    }
}
