using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService;
using Zip.Services.Accounts.Contract.Account;
using Zip.Services.Accounts.Contract.Account.Status;
using Zip.Services.Accounts.Contract.Invalidation;
using Zip.Services.Accounts.Contract.Order;
using Zip.Services.Accounts.ServiceProxy;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class AccountsServiceTests : CommonTestsFixture
    {
        private readonly AccountsService _target;

        private readonly Mock<IAccountsProxy> _accountsProxy;

        public AccountsServiceTests()
        {
            _accountsProxy = new Mock<IAccountsProxy>();
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<AccountsService>>();

            _target = new AccountsService(_accountsProxy.Object, logger.Object, mapper.Object);
        }

        [Fact]
        public async Task On_Ping_Should_Call_AccountsProxy_Ping()
        {
            // Arrange & Act
            await _target.Ping();

            // Assert
            _accountsProxy.Verify(x => x.Ping(), Times.Once);
        }

        [Fact]
        public async Task On_GetAccount_Should_Call_AccountsProxy_GetAccount()
        {
            // Arrange
            var accountId = Fixture.Create<long>()
                                   .ToString();

            // Act
            await _target.GetAccount(accountId);

            // Assert
            _accountsProxy.Verify(x => x.GetAccount(It.Is<string>(y => y == accountId)), Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_GetAccount_Should_Throw()
        {
            // Arrange
            var accountId = Fixture.Create<long>()
                                   .ToString();

            _accountsProxy.Setup(x => x.GetAccount(It.Is<string>(y => y == accountId)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(async () => await _target.GetAccount(accountId));
        }

        [Fact]
        public async Task On_UpdateConfiguration_Should_Call_AccountsProxy_UpdateConfiguration()
        {
            // Arrange
            var updateConfigurationRequest = Fixture.Create<UpdateConfigurationRequest>();

            // Act
            await _target.UpdateConfiguration(updateConfigurationRequest);

            // Assert
            _accountsProxy.Verify(x => x.UpdateConfiguration(
                                      It.Is<long>(y => y == updateConfigurationRequest.AccountId),
                                      It.Is<UpdateConfigurationRequest>(y => y == updateConfigurationRequest)),
                                  Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_UpdateConfiguration_Should_Throw()
        {
            // Arrange
            var updateConfigurationRequest = Fixture.Create<UpdateConfigurationRequest>();

            _accountsProxy.Setup(x => x.UpdateConfiguration(
                                     It.Is<long>(y => y == updateConfigurationRequest.AccountId),
                                     It.Is<UpdateConfigurationRequest>(y => y == updateConfigurationRequest)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(
                async () => await _target.UpdateConfiguration(updateConfigurationRequest));
        }

        [Fact]
        public async Task On_CloseAccount_Should_Call_AccountsProxy_CloseAccount()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var correlationId = Fixture.Create<string>();

            // Act
            await _target.CloseAccount(accountId, correlationId);

            // Assert
            _accountsProxy.Verify(x => x.CloseAccount(
                                      It.Is<long>(y => y == accountId),
                                      It.Is<string>(y => y == correlationId)),
                                  Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_CloseAccount_Should_Throw()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var correlationId = Fixture.Create<string>();

            _accountsProxy.Setup(x => x.CloseAccount(
                                     It.Is<long>(y => y == accountId),
                                     It.Is<string>(y => y == correlationId)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(
                async () => await _target.CloseAccount(accountId, correlationId));
        }

        [Fact]
        public async Task On_CloseAccount_With_CloseAccountRequest_Should_Call_AccountsProxy_CloseAccount()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var closeAccountRequest = Fixture.Create<CloseAccountRequest>();
            var correlationId = Fixture.Create<string>();

            // Act
            await _target.CloseAccount(accountId, closeAccountRequest, correlationId);

            // Assert
            _accountsProxy.Verify(x => x.CloseAccount(
                                      It.Is<long>(y => y == accountId),
                                      It.Is<CloseAccountRequest>(y => y == closeAccountRequest),
                                      It.Is<string>(y => y == correlationId)),
                                  Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_CloseAccount_With_CloseAccountRequest_Should_Throw()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var closeAccountRequest = Fixture.Create<CloseAccountRequest>();
            var correlationId = Fixture.Create<string>();

            _accountsProxy.Setup(x => x.CloseAccount(
                                     It.Is<long>(y => y == accountId),
                                     It.Is<CloseAccountRequest>(y => y == closeAccountRequest),
                                     It.Is<string>(y => y == correlationId)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(
                async () => await _target.CloseAccount(accountId, closeAccountRequest, correlationId));
        }

        [Fact]
        public async Task On_LockAccount_Should_Call_AccountsProxy_LockAccount()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var lockAccountRequest = Fixture.Create<LockAccountRequest>();

            // Act
            await _target.LockAccount(accountId, lockAccountRequest);

            // Assert
            _accountsProxy.Verify(x => x.LockAccount(
                                      It.Is<long>(y => y == accountId),
                                      It.Is<LockAccountRequest>(y => y == lockAccountRequest)),
                                  Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_LockAccount_Should_Throw()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var lockAccountRequest = Fixture.Create<LockAccountRequest>();

            _accountsProxy.Setup(x => x.LockAccount(
                                     It.Is<long>(y => y == accountId),
                                     It.Is<LockAccountRequest>(y => y == lockAccountRequest)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(
                async () => await _target.LockAccount(accountId, lockAccountRequest));
        }

        [Fact]
        public async Task On_Invalidate_Should_Call_AccountsProxy_Invalidate()
        {
            // Arrange
            var invalidationRequest = Fixture.Create<InvalidationRequest>();

            // Act
            await _target.Invalidate(invalidationRequest);

            // Assert
            _accountsProxy.Verify(x => x.Invalidate(
                                      It.Is<InvalidationRequest>(y => y == invalidationRequest)),
                                  Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_Invalidate_Should_Throw()
        {
            // Arrange
            var invalidationRequest = Fixture.Create<InvalidationRequest>();

            _accountsProxy.Setup(x => x.Invalidate(
                                     It.Is<InvalidationRequest>(y => y == invalidationRequest)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(async () => await _target.Invalidate(invalidationRequest));
        }

        [Fact]
        public async Task On_GetPayoutQuote_Should_Call_Accounts_Proxy_GetPayoutQuote()
        {
            // Arrange
            var accountId = Fixture.Create<long>();

            // Act
            await _target.GetPayoutQuote(accountId);

            // Assert
            _accountsProxy.Verify(x => x.GetPayoutQuote(It.Is<long>(y => y == accountId)), Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_GetPayoutQuote_Should_Throw()
        {
            // Arrange
            var accountId = Fixture.Create<long>();

            _accountsProxy.Setup(x => x.GetPayoutQuote(
                                     It.Is<long>(y => y == accountId)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(async () => await _target.GetPayoutQuote(accountId));
        }

        [Fact]
        public async Task On_GetTangoTransactions_Should_Call_Accounts_Proxy_GetTangoTransactions()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var startDate = Fixture.Create<DateTime>();
            var endDate = startDate.AddDays(1);
            var includeAuthorisedTransaction = Fixture.Create<bool>();

            // Act
            await _target.GetTangoTransactions(accountId, startDate, endDate, includeAuthorisedTransaction);

            // Assert
            _accountsProxy.Verify(x => x.GetTangoTransactions(
                                      It.Is<long>(y => y == accountId),
                                      It.Is<DateTime>(y => y == startDate),
                                      It.Is<DateTime>(y => y == endDate),
                                      It.Is<bool>(y => y == includeAuthorisedTransaction)),
                                  Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_GetTangoTransactions_Should_Throw()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var startDate = Fixture.Create<DateTime>();
            var endDate = startDate.AddDays(1);
            var includeAuthorisedTransaction = Fixture.Create<bool>();

            _accountsProxy.Setup(x => x.GetTangoTransactions(
                                     It.Is<long>(y => y == accountId),
                                     It.Is<DateTime>(y => y == startDate),
                                     It.Is<DateTime>(y => y == endDate),
                                     It.Is<bool>(y => y == includeAuthorisedTransaction)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(
                async () => await _target.GetTangoTransactions(accountId,
                                                               startDate,
                                                               endDate,
                                                               includeAuthorisedTransaction));
        }

        [Fact]
        public async Task On_GetOrders_Should_Call_Accounts_Proxy_GetOrders()
        {
            // Arrange
            var accountId = Fixture.Create<long>();

            // Act
            await _target.GetOrders(accountId);

            // Assert
            _accountsProxy.Verify(x => x.GetOrders(It.Is<long>(y => y == accountId)), Times.Once());
        }

        [Fact]
        public async Task Given_Exception_On_GetOrders_Should_Throw()
        {
            // Arrange
            var accountId = Fixture.Create<long>();

            _accountsProxy.Setup(x => x.GetOrders(
                                     It.Is<long>(y => y == accountId)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(async () => await _target.GetOrders(accountId));
        }

        [Fact]
        public async Task On_GetOrderDetail_Should_Call_Accounts_Proxy_GetOrderDetail()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var orderId = Fixture.Create<long>();

            // Act
            await _target.GetOrderDetail(accountId, orderId);

            // Assert
            _accountsProxy.Verify(x => x.GetOrderDetail(
                                      It.Is<long>(y => y == accountId),
                                      It.Is<long>(y => y == orderId)),
                                  Times.Once());
        }

        [Fact]
        public async Task Given_Exception_On_GetOrderDetail_Should_Throw()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var orderId = Fixture.Create<long>();

            _accountsProxy.Setup(x => x.GetOrderDetail(
                                     It.Is<long>(y => y == accountId),
                                     It.Is<long>(y => y == orderId)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(
                async () => await _target.GetOrderDetail(accountId, orderId));
        }

        [Fact]
        public async Task On_TransferOrderBalance_Should_Call_Accounts_Proxy_TransferOrderBalance()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var orderId = Fixture.Create<long>();
            var transferOrderRequest = Fixture.Build<TransferOrderRequest>()
                                              .With(x => x.TransferType, TransferType.ToInterestBearing)
                                              .Create();

            _accountsProxy.Setup(x => x.TransferOrderBalance(
                                     It.Is<long>(y => y == accountId),
                                     It.Is<long>(y => y == orderId),
                                     It.Is<TransferOrderRequest>(y => y == transferOrderRequest)))
                          .Returns(Task.CompletedTask);

            // Act
            await _target.TransferOrderBalance(accountId, orderId, transferOrderRequest);

            // Assert
            _accountsProxy.Verify(x => x.TransferOrderBalance(
                                      It.Is<long>(y => y == accountId),
                                      It.Is<long>(y => y == orderId),
                                      It.Is<TransferOrderRequest>(y => y == transferOrderRequest)),
                                  Times.Once());
        }

        [Fact]
        public async Task Given_Exception_On_TransferOrderBalance_Should_Throw()
        {
            // Arrange
            var accountId = Fixture.Create<long>();
            var orderId = Fixture.Create<long>();
            var transferOrderRequest = Fixture.Build<TransferOrderRequest>()
                                              .With(x => x.TransferType, TransferType.ToInterestBearing)
                                              .Create();

            _accountsProxy.Setup(x => x.TransferOrderBalance(
                                     It.Is<long>(y => y == accountId),
                                     It.Is<long>(y => y == orderId),
                                     It.Is<TransferOrderRequest>(y => y == transferOrderRequest)))
                          .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<AccountsApiException>(
                async () => await _target.TransferOrderBalance(accountId, orderId, transferOrderRequest));
        }
    }
}