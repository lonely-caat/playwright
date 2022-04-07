using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPayment;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Payments
{
    public class GetPaymentQueryHandlerTest
    {
        private readonly Mock<IPaymentsServiceProxy> _paymentsServiceProxy;
        private readonly Mock<IMapper> _mapper;

        public GetPaymentQueryHandlerTest()
        {
            _paymentsServiceProxy = new Mock<IPaymentsServiceProxy>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetPaymentQueryHandler(null, _mapper.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetPaymentQueryHandler(_paymentsServiceProxy.Object, null);
            });
        }

        [Fact]
        public async Task Given_Response_ShouldReturn()
        {
            _paymentsServiceProxy.Setup(x => x.GetPayment(It.IsAny<string>()));
            _mapper.Setup(x => x.Map<PaymentDto>(It.IsAny<object>()))
                .Returns(new PaymentDto());

            var handler = new GetPaymentQueryHandler(_paymentsServiceProxy.Object, _mapper.Object);
            var result = await handler.Handle(new GetPaymentQuery(), CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
