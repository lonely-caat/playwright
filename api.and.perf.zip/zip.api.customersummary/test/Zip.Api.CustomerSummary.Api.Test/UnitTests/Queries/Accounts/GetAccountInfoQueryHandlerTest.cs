using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Mapper;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Account;
using static Zip.Api.CustomerSummary.Domain.Entities.Tango.LoanMgt;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Accounts
{
    public class GetAccountInfoQueryHandlerTest
    {
        private readonly GetAccountInfoQueryHandler _target;
        
        private readonly Mock<IConsumerContext> _consumerContext;

        private readonly Mock<ICustomerAttributeContext> _customerAttributeContext;

        private readonly Mock<IAccountTypeContext> _accountTypeContext;

        private readonly Mock<IAccountsService> _accountsService;

        private readonly IMapper _mapper;

        private readonly Fixture _fixture;

        public GetAccountInfoQueryHandlerTest()
        {
            _consumerContext = new Mock<IConsumerContext>();
            _customerAttributeContext = new Mock<ICustomerAttributeContext>();
            _accountTypeContext = new Mock<IAccountTypeContext>();
            _accountsService = new Mock<IAccountsService>();
            _fixture = new Fixture();

            var configurationProvider = new MapperConfiguration(cfg => cfg.AddMaps(typeof(AccountsProfile)));

            _mapper = configurationProvider.CreateMapper();

            _target = new GetAccountInfoQueryHandler(
                _consumerContext.Object,
                _customerAttributeContext.Object,
                _accountTypeContext.Object,
                _accountsService.Object,
                _mapper);
        }

        [Fact]
        public void Given_NullConsumerContext_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetAccountInfoQueryHandler(
                    null,
                    _customerAttributeContext.Object,
                    _accountTypeContext.Object,
                    _accountsService.Object,
                    _mapper);
            });
        }

        [Fact]
        public void Given_NullConsumerAttributeContext_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetAccountInfoQueryHandler(
                    _consumerContext.Object,
                    null,
                    _accountTypeContext.Object,
                    _accountsService.Object,
                    _mapper);
            });
        }

        [Fact]
        public void Given_NullAccountTypeContext_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetAccountInfoQueryHandler(
                    _consumerContext.Object,
                    _customerAttributeContext.Object,
                    null,
                    _accountsService.Object,
                    _mapper);
            });
        }

        [Fact]
        public void Given_NullMapper_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetAccountInfoQueryHandler(
                    _consumerContext.Object,
                    _customerAttributeContext.Object,
                    _accountTypeContext.Object,
                    _accountsService.Object,
                    null);
            });
        }

        [Fact]
        public async Task Given_NullAccountInfo_ShouldThrow_AccountNotFoundException()
        {
            _consumerContext.Setup(x => x.GetAccountInfoAsync(It.IsAny<long>()))
                            .ReturnsAsync(null as AccountInfo);

            await Assert.ThrowsAsync<AccountNotFoundException>(async () =>
            {
                await _target.Handle(new GetAccountInfoQuery(), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_ErrorInAccountsProxy_WhenCall_GetAccount_ShouldNotThrow_AnyException()
        {
            _consumerContext.Setup(x => x.GetAccountInfoAsync(It.IsAny<long>()))
                            .ReturnsAsync(new AccountInfo());

            _accountsService.Setup(x => x.GetAccount(It.IsAny<string>()))
                          .ThrowsAsync(new Exception());

            _accountsService.Setup(x => x.GetPayoutQuote(It.IsAny<long>()))
                          .ThrowsAsync(new Exception());

            var result = await _target.Handle(new GetAccountInfoQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Null(result.LmsAccount);
            Assert.Null(result.PayoutQuote);
        }

        [Theory]
        [InlineData(AccountState.Active, null, true, OperationalStatus)]
        [InlineData(AccountState.Active, null, null, NewAccountStatus)]
        [InlineData(AccountState.Active, null, false, NewAccountStatus)]
        [InlineData(AccountState.Closed, null, null, "GeneralPurpose1")]
        [InlineData(AccountState.ChargedOff, null, null, "GeneralPurpose1")]
        [InlineData(AccountState.Locked, AccountSubState.Fraud, null, NoFurtherDrawDownStatus)]
        [InlineData(AccountState.Locked, AccountSubState.Other, null, NoFurtherDrawDownStatus)]
        [InlineData(AccountState.Locked, AccountSubState.Arrears, null, OperationalStatus)]
        [InlineData(AccountState.Locked, AccountSubState.NotSet, null, "GeneralPurpose1")]
        [InlineData(AccountState.Locked, null, null, "GeneralPurpose1")]
        public async Task Given_AccountResponse_Handle_Should_Return_LmsAccount_Correctly(
            AccountState state,
            AccountSubState? subState,
            bool? hasTransacted,
            string expectedCreditStatus)
        {
            // Arrange
            var accountInfo = _fixture.Create<AccountInfo>();
            var loanMgtAccount = _fixture.Build<Services.Accounts.Contract.Tango.LoanMgtAccount>()
                                         .With(x => x.ContractualNextDueDateAsAt, DateTime.Now.ToString)
                                         .With(x => x.DirectDebitNextDateDueAsAt, DateTime.Now.ToString)
                                         .With(x => x.GeneralPurpose1, expectedCreditStatus)
                                         .Create();
            var accountResponse = _fixture.Build<AccountResponse>()
                                          .With(x => x.State, state)
                                          .With(x => x.SubState, subState)
                                          .With(x => x.HasTransacted, hasTransacted)
                                          .Create();
            accountResponse.LoanMgtAccount = loanMgtAccount;
            
            var payoutQuote = _fixture.Create<decimal>();

            _consumerContext.Setup(x => x.GetAccountInfoAsync(It.IsAny<long>()))
                            .ReturnsAsync(accountInfo);

            _accountsService.Setup(x => x.GetAccount(It.IsAny<string>()))
                          .ReturnsAsync(accountResponse);

            _accountsService.Setup(x => x.GetPayoutQuote(It.IsAny<long>()))
                          .ReturnsAsync(payoutQuote);

            // Act
            var actual = await _target.Handle(new GetAccountInfoQuery(), CancellationToken.None);

            // Assert
            actual.LmsAccount.Should().NotBeNull();
            actual.LmsAccount.CreditStatus.Should().Be(expectedCreditStatus);
        }
    }
}
