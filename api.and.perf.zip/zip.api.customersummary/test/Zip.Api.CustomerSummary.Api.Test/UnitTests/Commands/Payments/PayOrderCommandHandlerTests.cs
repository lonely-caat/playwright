using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Payments.Command.PayOrder;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Request.PayOrder;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Payments
{
    public class PayOrderCommandHandlerTests : CommonTestsFixture
    {
        private readonly PayOrderCommandHandler _target;

        private readonly Mock<ICoreGraphService> _coreGraphService;

        public PayOrderCommandHandlerTests()
        {
            _coreGraphService = new Mock<ICoreGraphService>();
            var logger = new Mock<ILogger<PayOrderCommandHandler>>();

            _target = new PayOrderCommandHandler(_coreGraphService.Object, logger.Object);
        }

        [Fact]
        public async Task Given_Valid_Request_Should_Call_CoreGraphService_PayOrderAsync()
        {
            // Arrange
            var request = Fixture.Create<PayOrderCommand>();

            _coreGraphService.Setup(x => x.PayOrderAsync(It.Is<PayOrderInput>(
                                                             y => y.AccountId == request.AccountId.ToString() &&
                                                                  y.CustomerId == request.CustomerId &&
                                                                  y.OrderId == request.OrderId &&
                                                                  y.Amount == request.Amount)))
                             .ReturnsAsync(Fixture.Create<PayOrderInnerResponse>());

            // Act
            var result = await _target.Handle(request, default);

            // Assert
            _coreGraphService.Verify(x => x.PayOrderAsync(It.Is<PayOrderInput>(
                                                              y => y.AccountId == request.AccountId.ToString() &&
                                                                   y.CustomerId == request.CustomerId &&
                                                                   y.OrderId == request.OrderId &&
                                                                   y.Amount == request.Amount)),
                                     Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Given_Exception_On_CoreGraphService_PayOrderAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<PayOrderCommand>();

            _coreGraphService.Setup(x => x.PayOrderAsync(It.Is<PayOrderInput>(
                                                             y => y.AccountId == request.AccountId.ToString() &&
                                                                  y.CustomerId == request.CustomerId &&
                                                                  y.OrderId == request.OrderId &&
                                                                  y.Amount == request.Amount)))
                             .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _target.Handle(request, default));
        }
    }
}