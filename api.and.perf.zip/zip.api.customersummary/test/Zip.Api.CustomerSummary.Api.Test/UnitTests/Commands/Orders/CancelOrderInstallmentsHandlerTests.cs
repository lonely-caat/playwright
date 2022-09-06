using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Orders.Command;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Services.Accounts.Contract.Order;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Orders
{
    public class CancelOrderInstallmentsCommandHandlerTests : CommonTestsFixture
    {
        private readonly CancelOrderInstallmentsCommandHandler _target;

        private readonly Mock<IAccountsService> _accountsService;
        
        public CancelOrderInstallmentsCommandHandlerTests()
        {
            _accountsService = new Mock<IAccountsService>();
            var _logger = new Mock<ILogger<CancelOrderInstallmentsCommandHandler>>();
            _target = new CancelOrderInstallmentsCommandHandler(_accountsService.Object, _logger.Object);
        }

        [Fact]
        public async Task Given_ValidRequest_Should_Call_AccountsService_TransferOrderBalance()
        {
            // Arrange
            var request = Fixture.Create<CancelOrderInstallmentsCommand>();

            _accountsService.Setup(x => x.TransferOrderBalance(
                                       It.Is<long>(y => y == request.AccountId),
                                       It.Is<long>(y => y == request.OrderId),
                                       It.Is<TransferOrderRequest>(y => y.TransferType == TransferType.ToInterestBearing)))
                            .Returns(Task.CompletedTask);

            // Act
            await _target.Handle(request, default);

            // Assert
            _accountsService.Verify(x => x.TransferOrderBalance(It.Is<long>(y => y == request.AccountId),
                                                                It.Is<long>(y => y == request.OrderId),
                                                                It.Is<TransferOrderRequest>(y => y.TransferType == TransferType.ToInterestBearing)),
                                    Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_UpdateConfiguration_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<CancelOrderInstallmentsCommand>();

            _accountsService.Setup(x => x.TransferOrderBalance(
                                       It.Is<long>(y => y == request.AccountId),
                                       It.Is<long>(y => y == request.OrderId),
                                       It.Is<TransferOrderRequest>(y => y.TransferType == TransferType.ToInterestBearing)))
                            .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _target.Handle(request, default));
        }
    }
}

