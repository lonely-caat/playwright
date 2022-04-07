using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetDefaultPaymentMethod;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetLatestPaymentMethods;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Payments
{
    public class GetDefaultPaymentMethodQueryHandlerTest
    {
        private readonly Mock<IMediator> _mediator;

        public GetDefaultPaymentMethodQueryHandlerTest()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetDefaultPaymentMethodQueryHandler(null);
            });
        }

        [Fact]
        public async Task Given_NoPaymentMethodsFound_ShouldReturn_Null()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetLatestPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PaymentMethodDto>());

            var handler = new GetDefaultPaymentMethodQueryHandler(_mediator.Object);
            var result = await handler.Handle(new GetDefaultPaymentMethodQuery(), CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Given_PaymentMethodsFound_ShouldReturn_FirstDefaultOne()
        {
            var expentedId = Guid.NewGuid();

            _mediator.Setup(x => x.Send(It.IsAny<GetLatestPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PaymentMethodDto>() {
                    new PaymentMethodDto()
                    {
                        Id = Guid.NewGuid(),
                        IsDefault = false
                    },
                    new PaymentMethodDto()
                    {
                        Id= expentedId,
                        IsDefault = true
                    },
                    new PaymentMethodDto()
                    {
                        Id= Guid.NewGuid(),
                        IsDefault = true
                    }
                });

            var handler = new GetDefaultPaymentMethodQueryHandler(_mediator.Object);
            var result = await handler.Handle(new GetDefaultPaymentMethodQuery(), CancellationToken.None);

            Assert.Equal(expentedId, result.Id);
        }
    }
}
