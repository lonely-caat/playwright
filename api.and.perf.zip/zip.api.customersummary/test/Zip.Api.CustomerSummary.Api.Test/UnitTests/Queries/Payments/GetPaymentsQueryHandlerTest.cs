using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPayments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Payments
{
    public class GetPaymentsQueryHandlerTest
    {
        private readonly Mock<IPaymentsServiceProxy> _paymentsServiceProxy;
        private readonly Mock<IMapper> _mapper;

        public GetPaymentsQueryHandlerTest()
        {
            _paymentsServiceProxy = new Mock<IPaymentsServiceProxy>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GetPaymentsQueryHandler(null, _mapper.Object));
            Assert.Throws<ArgumentNullException>(() => new GetPaymentsQueryHandler(_paymentsServiceProxy.Object, null));
        }

        [Fact]
        public async Task Given_NoPaymentsFound_ShouldReturn_Null()
        {
            _paymentsServiceProxy.Setup(x => x.GetAllPayments(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<Guid?>()));
            _mapper.Setup(x => x.Map<IEnumerable<PaymentDto>>(It.IsAny<object>()))
                .Returns(null as IEnumerable<PaymentDto>);

            var handler = new GetPaymentsQueryHandler(_paymentsServiceProxy.Object, _mapper.Object);
            var result = await handler.Handle(new GetPaymentsQuery(), CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Given_Payments_ShouldReturn_OrderByCreatedDateTimeLocal()
        {
            _paymentsServiceProxy.Setup(x => x.GetAllPayments(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<Guid?>()));
            _mapper.Setup(x => x.Map<IEnumerable<PaymentDto>>(It.IsAny<object>()))
                .Returns(new List<PaymentDto>() { 
                new PaymentDto(){ 
                    CountryCode = ZipMoney.Services.Payments.Contract.Types.CountryCode.NZ,
                    CreatedDateTime = DateTime.Now.AddDays(-2),
                },
                new PaymentDto(){
                CountryCode = ZipMoney.Services.Payments.Contract.Types.CountryCode.AU,
                    CreatedDateTime = DateTime.Now.AddDays(-1),
                } });

            var handler = new GetPaymentsQueryHandler(_paymentsServiceProxy.Object, _mapper.Object);
            var result = await handler.Handle(new GetPaymentsQuery(), CancellationToken.None);

            Assert.Equal(ZipMoney.Services.Payments.Contract.Types.CountryCode.AU, result.First().CountryCode);
        }
    }
}
