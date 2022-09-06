using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetLatestPaymentMethods;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethods;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Payments
{
    public class GetLatestPaymentMethodsQueryHandlerTest
    {
        private readonly Mock<IMediator> _mediator;

        public GetLatestPaymentMethodsQueryHandlerTest()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetLatestPaymentMethodsQueryHandler(null);
            });
        }

        [Fact]
        public async Task Given_NoPaymentMethodsFound_ShouldReturn_NUll()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PaymentMethodDto>());

            var handler = new GetLatestPaymentMethodsQueryHandler(_mediator.Object);
            var result = await handler.Handle(new GetLatestPaymentMethodsQuery(), CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Given_MultipleSameTypePaymentMethodsFound_ShouldReturn_OnlyOne()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PaymentMethodDto>() {
                    new PaymentMethodDto()
                    {
                        Type = ZipMoney.Services.Payments.Contract.Types.PaymentMethodType.BankAccount,
                        CreatedTimeStamp = new DateTime(2019,10,1)
                    },
                    new PaymentMethodDto()
                    {
                        Type = ZipMoney.Services.Payments.Contract.Types.PaymentMethodType.Bpay,
                        CreatedTimeStamp = new DateTime(2019,9,1)
                    },
                    new PaymentMethodDto()
                    {
                        Type = ZipMoney.Services.Payments.Contract.Types.PaymentMethodType.BankAccount,
                        CreatedTimeStamp = new DateTime(2019,9,1)
                    }
                }); ; ;

            var handler = new GetLatestPaymentMethodsQueryHandler(_mediator.Object);
            var result = await handler.Handle(new GetLatestPaymentMethodsQuery(), CancellationToken.None);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Given_MultipleSameTimestamp_With_Default_ShouldReturn_Default_Or_Latest()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PaymentMethodDto>() {
                    new PaymentMethodDto()
                    {
                        AccountName = "Non-default latest",
                        Type = ZipMoney.Services.Payments.Contract.Types.PaymentMethodType.BankAccount,
                        CreatedTimeStamp = new DateTime(2019,11,1)
                    },
                    new PaymentMethodDto()
                    {
                        AccountName = "Default",
                        Type = ZipMoney.Services.Payments.Contract.Types.PaymentMethodType.BankAccount,
                        IsDefault = true,
                        CreatedTimeStamp = new DateTime(2019,10,1)
                    },
                    new PaymentMethodDto()
                    {
                        AccountName = "Non-Default old",
                        Type = ZipMoney.Services.Payments.Contract.Types.PaymentMethodType.BankAccount,
                        CreatedTimeStamp = new DateTime(2019,10,1)
                    }
                }) ; ; ;

            var handler = new GetLatestPaymentMethodsQueryHandler(_mediator.Object);
            var result = await handler.Handle(new GetLatestPaymentMethodsQuery(), CancellationToken.None);

            Assert.Single(result);
            Assert.Equal("Default", result.First().AccountName);
        }

        [Fact]
        public async Task Given_MultipleSameTimestamp_Without_Default_ShouldReturn__Latest()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PaymentMethodDto>() {
                    new PaymentMethodDto()
                    {
                        AccountName = "Non-default latest",
                        Type = ZipMoney.Services.Payments.Contract.Types.PaymentMethodType.BankAccount,
                        CreatedTimeStamp = new DateTime(2019,10,1)
                    },
                    new PaymentMethodDto()
                    {
                        AccountName = "Non-default old",
                        Type = ZipMoney.Services.Payments.Contract.Types.PaymentMethodType.BankAccount,
                        CreatedTimeStamp = new DateTime(2019,9,1)
                    }
                }); ; ;

            var handler = new GetLatestPaymentMethodsQueryHandler(_mediator.Object);
            var result = await handler.Handle(new GetLatestPaymentMethodsQuery(), CancellationToken.None);

            Assert.Single(result);
            Assert.Equal("Non-default latest", result.First().AccountName);
        }
    }
}
