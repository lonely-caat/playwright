using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethods;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using ZipMoney.Services.Payments.Contract.PaymentMethods;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Payments
{
    public class GetPaymentMethodsQueryHandlerTest
    {
        private readonly Mock<IPaymentsServiceProxy> _paymentsServiceProxy;
        private readonly Mock<IMapper> _mapper;

        public GetPaymentMethodsQueryHandlerTest()
        {
            _paymentsServiceProxy = new Mock<IPaymentsServiceProxy>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetPaymentMethodsQueryHandler(null, _mapper.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
          {
              new GetPaymentMethodsQueryHandler(_paymentsServiceProxy.Object, null);
          });
        }

        [Fact]
        public async Task Given_NoState_ShouldNot_Filter()
        {
            _paymentsServiceProxy
               .Setup(x =>
                   x.GetAllPaymentMethods(
                       It.IsAny<string>(),
                       It.IsAny<bool>()))
               .ReturnsAsync(null as ReadOnlyCollection<PaymentMethodResponse>);

            _mapper.Setup(x =>
                x.Map<IEnumerable<PaymentMethodDto>>(It.IsAny<object>()))
                .Returns(new List<PaymentMethodDto>()
                {
                    new PaymentMethodDto()
                    {
                         AccountName = "Shan",
                         State = ZipMoney.Services.Payments.Contract.Types.PaymentMethodState.Approved
                    },
                    new PaymentMethodDto()
                    {
                        AccountName = "Shan",
                        State = ZipMoney.Services.Payments.Contract.Types.PaymentMethodState.Removed
                    }
                });


            var handler = new GetPaymentMethodsQueryHandler(_paymentsServiceProxy.Object, _mapper.Object);
            var result = await handler.Handle(new GetPaymentMethodsQuery(), CancellationToken.None);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Given_State_Should_FilterResults()
        {
            _paymentsServiceProxy
               .Setup(x =>
                   x.GetAllPaymentMethods(
                       It.IsAny<string>(),
                       It.IsAny<bool>()))
               .ReturnsAsync(null as ReadOnlyCollection<PaymentMethodResponse>);

            _mapper.Setup(x =>
                x.Map<IEnumerable<PaymentMethodDto>>(It.IsAny<object>()))
                .Returns(new List<PaymentMethodDto>()
                {
                    new PaymentMethodDto()
                    {
                         AccountName = "Shan",
                         State = ZipMoney.Services.Payments.Contract.Types.PaymentMethodState.Approved
                    },
                    new PaymentMethodDto()
                    {
                        AccountName = "Shan",
                        State = ZipMoney.Services.Payments.Contract.Types.PaymentMethodState.Removed
                    }
                });


            var handler = new GetPaymentMethodsQueryHandler(_paymentsServiceProxy.Object, _mapper.Object);
            var result = await handler.Handle(new GetPaymentMethodsQuery() { 
            State = "approved"}, CancellationToken.None);

            Assert.Single(result);
        }
    }
}
