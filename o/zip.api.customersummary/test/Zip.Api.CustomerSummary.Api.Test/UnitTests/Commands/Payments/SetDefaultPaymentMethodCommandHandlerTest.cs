using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.SetDefaultPaymentMethod;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Payments
{
    public class SetDefaultPaymentMethodCommandHandlerTest
    {
        private readonly Mock<IPaymentsServiceProxy> paymentsServiceProxy = new Mock<IPaymentsServiceProxy>();

        [Fact]
        public void Given_NullProxy_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SetDefaultPaymentMethodCommandHandler(null);
            });
        }

        [Fact]
        public async Task Given_AllGood()
        {
            paymentsServiceProxy.Setup(x => x.SetDefaultPaymentMethod(It.IsAny<Guid>(), It.IsAny<string>()));
            var handler = new SetDefaultPaymentMethodCommandHandler(paymentsServiceProxy.Object);

            var result = await handler.Handle(new SetDefaultPaymentMethodCommand(), CancellationToken.None);
            Assert.Equal(Unit.Value, result);
        }
    }
}
