using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethod;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Payments
{
    public class GetPaymentMethodQueryHandlerTest
    {
        private readonly Mock<IPaymentsServiceProxy> _paymentsServiceProxy;
        private readonly Mock<IMapper> _mapper;

        public GetPaymentMethodQueryHandlerTest()
        {
            _paymentsServiceProxy = new Mock<IPaymentsServiceProxy>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetPaymentMethodQueryHandler(null, _mapper.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetPaymentMethodQueryHandler(_paymentsServiceProxy.Object, null);
            });
        }

        [Fact]
        public async Task Should_return()
        {

            _paymentsServiceProxy.Setup(x => x.GetPaymentMethod(It.IsAny<Guid>()))
                .ReturnsAsync(new ZipMoney.Services.Payments.Contract.PaymentMethods.PaymentMethodResponse()
                {
                    AccountName = "Shan"
                });

            _mapper.Setup(x => x.Map<PaymentMethodDto>(It.IsAny<object>()))
                .Returns(new PaymentMethodDto() { AccountName = "Shan" });

            var handler = new GetPaymentMethodQueryHandler(_paymentsServiceProxy.Object, _mapper.Object);
            var paymentMethod = await handler.Handle(new GetPaymentMethodQuery(), CancellationToken.None);

            Assert.Equal("Shan", paymentMethod.AccountName);
        }
    }
}
