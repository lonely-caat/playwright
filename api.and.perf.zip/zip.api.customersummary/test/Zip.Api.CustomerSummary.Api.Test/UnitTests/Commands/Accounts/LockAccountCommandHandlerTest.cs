using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm.Models;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Account;
using Zip.Services.Accounts.Contract.Account.Status;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Accounts
{
    public class LockAccountCommandHandlerTest
    {
        private readonly LockAccountCommandHandler _handler;
        private readonly Mock<IAccountsService> _accountsService;
        private readonly Mock<IAttributeContext> _attributeContext;
        private readonly Mock<IConsumerContext> _consumerContext;
        private readonly Mock<ICrmServiceProxy> _crmServiceProxy;

        public LockAccountCommandHandlerTest()
        {
            _accountsService = new Mock<IAccountsService>();
            _attributeContext = new Mock<IAttributeContext>();
            _consumerContext = new Mock<IConsumerContext>();
            _crmServiceProxy = new Mock<ICrmServiceProxy>();

            _handler = new LockAccountCommandHandler(_accountsService.Object, _attributeContext.Object, _consumerContext.Object, _crmServiceProxy.Object);
        }
        
        [Fact]
        public async Task Given_DependencyInjection_Null_ShouldThrow_ArgumentNullException()
        {

            await Assert.ThrowsAsync<ArgumentNullException>(
                async () =>
                {
                    var handler = new LockAccountCommandHandler(null, null, null, null);
                    await handler.Handle(It.IsAny<LockAccountCommand>(), default);
                });
        }

        [Fact]
        public async Task Given_InvalidRequest_ShouldThrow_Exception()
        {
            await Assert.ThrowsAsync<NullReferenceException>(
                async () =>
                {
                    await _handler.Handle(It.IsAny<LockAccountCommand>(), default);
                });

            await Assert.ThrowsAsync<AccountNotFoundException>(
                async () =>
                {
                    var request = new LockAccountCommand()
                    {
                        AccountId = It.IsAny<long>(),
                        ConsumerId = It.IsAny<long>(),
                        ChangedBy = It.IsAny<string>(),
                        Reason = It.IsAny<string>()
                    };
                    await _handler.Handle(request, default);
                });
        }

        [Fact]
        public async Task FailureToLockAccount_ShouldThrow_Exception()
        {
            _accountsService
               .Setup(x => x.GetAccount(It.IsAny<string>()))
               .ReturnsAsync(new AccountResponse() { State = AccountState.Active, SubState = AccountSubState.NotSet });

            _accountsService
                .Setup(x => x.LockAccount(It.IsAny<long>(), It.IsAny<LockAccountRequest>()))
                .ReturnsAsync(new LockAccountResponse() { State = AccountState.Active });

            await Assert.ThrowsAsync<LockAccountException>(
                async () =>
                {
                    var request = new LockAccountCommand()
                    {
                        AccountId = It.IsAny<long>(),
                        ConsumerId = It.IsAny<long>(),
                        ChangedBy = It.IsAny<string>(),
                        Reason = It.IsAny<string>()
                    };
                    await _handler.Handle(request, default);
                });
        }

        [Fact]
        public async Task Given_ValidRequest_Should_LockAccount()
        {
            _accountsService
               .Setup(x => x.GetAccount(It.IsAny<string>()))
               .ReturnsAsync(new AccountResponse() { State = AccountState.Active, SubState = AccountSubState.NotSet });

            _accountsService
                .Setup(x => x.LockAccount(It.IsAny<long>(), It.IsAny<LockAccountRequest>()))
                .ReturnsAsync(new LockAccountResponse() { State = AccountState.Locked, SubState = AccountSubState.Other });

            _attributeContext
                .Setup(x => x.GetConsumerAttributesAsync(It.IsAny<long>()))
                .ReturnsAsync(new List<ConsumerAttributeDto>());

            var request = new LockAccountCommand()
            {
                AccountId = 134,
                ConsumerId = 305,
                Reason = "Test lock account",
                ChangedBy = "alvin.ho@zip.co"
            };
            var result = await _handler.Handle(request, default);

            _accountsService
                .Verify(x =>
                    x.LockAccount(It.IsAny<long>(), It.IsAny<LockAccountRequest>()),
                    Times.Once);

            _attributeContext
                .Verify(x =>
                    x.SetConsumerAttributesAsync(It.IsAny<long>(), It.IsAny<List<long>>()),
                    Times.Once);

            _consumerContext
                .Verify(x =>
                    x.SetTrustScoreAsync(It.IsAny<long>(), It.IsAny<int>()),
                    Times.Once);

            _crmServiceProxy
                .Verify(x =>
                    x.CreateComment(It.IsAny<CreateCommentRequest>()),
                    Times.Once);

            Assert.Equal(Unit.Value, result);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ConsumerHasNfdAtrribute_ShouldNotAddAttribute_ShouldLockAccount()
        {
            _accountsService
               .Setup(x => x.GetAccount(It.IsAny<string>()))
               .ReturnsAsync(new AccountResponse() { State = AccountState.Active, SubState = AccountSubState.NotSet });

            _accountsService
                .Setup(x => x.LockAccount(It.IsAny<long>(), It.IsAny<LockAccountRequest>()))
                .ReturnsAsync(new LockAccountResponse() { State = AccountState.Locked, SubState = AccountSubState.Other });

            _attributeContext
                .Setup(x => x.GetConsumerAttributesAsync(It.IsAny<long>()))
                .ReturnsAsync(new List<ConsumerAttributeDto>()
                {
                    new ConsumerAttributeDto() {
                            Id = (int)ConsumerAttributeEnum.NoFurtherDrawdown,
                            Name = "No Further Drawdown"
                        }
                });

            var request = new LockAccountCommand()
            {
                AccountId = 134,
                ConsumerId = 305,
                Reason = "Test lock account",
                ChangedBy = "alvin.ho@zip.co"
            };
            var result = await _handler.Handle(request, default);

            _accountsService
                .Verify(x =>
                    x.LockAccount(It.IsAny<long>(), It.IsAny<LockAccountRequest>()),
                    Times.Once);

            _attributeContext
                .Verify(x =>
                    x.SetConsumerAttributesAsync(It.IsAny<long>(), It.IsAny<List<long>>()),
                    Times.Never);

            _consumerContext
                .Verify(x =>
                    x.SetTrustScoreAsync(It.IsAny<long>(), It.IsAny<int>()),
                    Times.Once);

            _crmServiceProxy
                .Verify(x =>
                    x.CreateComment(It.IsAny<CreateCommentRequest>()),
                    Times.Once);

            Assert.Equal(Unit.Value, result);
        }
    }
}
