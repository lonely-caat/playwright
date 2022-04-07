using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.Refund;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using ZipMoney.Services.Payments.Contract.Payments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Payments
{
    public class RefundCommandHandlerTest
    {
        [Fact]
        public async Task Given_DependencyInjection_Null_ShouldThrow_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () =>
                {
                    var handler = new RefundCommandHandler(null);
                    await handler.Handle(null, CancellationToken.None);
                });
        }

        [Fact]
        public async Task Given_Valid_Should_Refund()
        {
            var mockProxy = new Mock<IPaymentsServiceProxy>();
            mockProxy.Setup(x => x.RefundPayment(It.IsAny<Guid>())).ReturnsAsync(new PaymentRefundResponse());

            var handler = new RefundCommandHandler(mockProxy.Object);
            var result = await handler.Handle(new RefundCommand(Guid.NewGuid()), default);

            Assert.IsType<PaymentRefundResponse>(result);
        }

        [Fact]
        public async Task Given_ExceptionThrown_ShouldThrow()
        {
            var proxy = new Mock<IPaymentsServiceProxy>();
            proxy.Setup(x => x.RefundPayment(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var handler = new RefundCommandHandler(proxy.Object);
                await handler.Handle(new RefundCommand(Guid.NewGuid()), CancellationToken.None);
            });
        }

    }
}
