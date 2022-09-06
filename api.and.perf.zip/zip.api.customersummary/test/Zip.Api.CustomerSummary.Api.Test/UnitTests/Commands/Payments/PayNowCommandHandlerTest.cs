using System;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Payments.Command.PayNow;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetDefaultPaymentMethod;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using ZipMoney.Services.Payments.Contract.Payments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Payments
{
    public class PayNowCommandHandlerTest
    {
        [Fact]
        public async Task Given_DependencyInjection_Null_ShouldThrow_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () =>
                {
                    var handler = new PayNowCommandHandler(null, null);
                    await handler.Handle(null, default);
                });
        }

        [Fact]
        public async Task Given_InvalidConsumer_ShouldThrow_Exception()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), default))
                .ReturnsAsync(null as Consumer);
            var mockProxy = new Mock<IPaymentsServiceProxy>();

            var handler = new PayNowCommandHandler(mockMediator.Object, mockProxy.Object);
            await Assert.ThrowsAsync<ConsumerNotFoundException>(async () =>
            {
                await handler.Handle(new PayNowCommand(), default);
            });
        }


        [Fact]
        public async Task Given_InvalidAccount_ShouldThrow_Exception()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), default))
                .ReturnsAsync(new Consumer());
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), default))
                .ReturnsAsync(new GetAccountInfoQueryResult()
                {
                    AccountInfo = null
                });
            var mockProxy = new Mock<IPaymentsServiceProxy>();

            var handler = new PayNowCommandHandler(mockMediator.Object, mockProxy.Object);
            await Assert.ThrowsAsync<InvalidCountryCodeException>(async () =>
            {
                await handler.Handle(new PayNowCommand(), default);
            });
        }

        [Fact]
        public async Task Given_InvalidCountryCode_ShouldThrow_Exception()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), default))
                .ReturnsAsync(new Consumer()
                {
                    CountryId = "CN"
                });
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), default))
                .ReturnsAsync(new GetAccountInfoQueryResult()
                {
                    AccountInfo = new AccountInfo()
                    {

                    }
                });
            var mockProxy = new Mock<IPaymentsServiceProxy>();

            var handler = new PayNowCommandHandler(mockMediator.Object, mockProxy.Object);
            await Assert.ThrowsAsync<InvalidCountryCodeException>(async () =>
            {
                await handler.Handle(new PayNowCommand(), default);
            });
        }

        [Fact]
        public async Task Given_NoDefaultPaymentMethod_ShouldThrow_Exception()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), default))
                .ReturnsAsync(new Consumer()
                {
                    CountryId = "CN"
                });
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), default))
                .ReturnsAsync(new GetAccountInfoQueryResult()
                {
                    AccountInfo = new AccountInfo()
                    {

                    }
                });
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetDefaultPaymentMethodQuery>(), default))
                .ReturnsAsync(null as PaymentMethodDto);

            var mockProxy = new Mock<IPaymentsServiceProxy>();

            var handler = new PayNowCommandHandler(mockMediator.Object, mockProxy.Object);
            await Assert.ThrowsAsync<InvalidCountryCodeException>(async () =>
            {
                await handler.Handle(new PayNowCommand(), default);
            });
        }

        [Fact]
        public async Task Given_FailureReasonInResponse_ShouldThrow_Exception()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), default))
                .ReturnsAsync(new Consumer()
                {
                    CountryId = "AU"
                });
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), default))
                .ReturnsAsync(new GetAccountInfoQueryResult()
                {
                    AccountInfo = new AccountInfo()
                    {

                    }
                });
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetDefaultPaymentMethodQuery>(), default))
                .ReturnsAsync(new PaymentMethodDto() { });

            var mockProxy = new Mock<IPaymentsServiceProxy>();
            mockProxy
                .Setup(x => x.MakePayment(It.IsAny<MakePaymentRequest>()))
                .ReturnsAsync(new PaymentResponse()
                {
                    FailureCode = "XXX-001",
                    FailureReason = "Test"
                });

            var handler = new PayNowCommandHandler(mockMediator.Object, mockProxy.Object);
            await Assert.ThrowsAsync<MakePaymentFailedException>(async () =>
            {
                await handler.Handle(new PayNowCommand(), default);
            });
        }

        [Fact]
        public async Task Given_EverythingIsFInd_Should_Succeed()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), default))
                .ReturnsAsync(new Consumer()
                {
                    CountryId = "AU"
                });
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), default))
                .ReturnsAsync(new GetAccountInfoQueryResult()
                {
                    AccountInfo = new AccountInfo()
                    {

                    }
                });
            mockMediator
                .Setup(x => x.Send(It.IsAny<GetDefaultPaymentMethodQuery>(), default))
                .ReturnsAsync(new PaymentMethodDto() { });

            var mockProxy = new Mock<IPaymentsServiceProxy>();
            mockProxy
                .Setup(x => x.MakePayment(It.IsAny<MakePaymentRequest>()))
                .ReturnsAsync(new PaymentResponse()
                {
                });

            var handler = new PayNowCommandHandler(mockMediator.Object, mockProxy.Object);
            var result =await handler.Handle(new PayNowCommand(), default);

            Assert.Equal(Unit.Value, result);
        }
    }
}
