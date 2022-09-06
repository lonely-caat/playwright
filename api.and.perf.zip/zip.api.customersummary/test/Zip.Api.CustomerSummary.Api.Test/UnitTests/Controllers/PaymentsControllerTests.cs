using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Payments.Command.PayNow;
using Zip.Api.CustomerSummary.Application.Payments.Command.PayOrder;
using Zip.Api.CustomerSummary.Application.Payments.Command.Refund;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPayment;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPayments;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetUpcomingInstallments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder;
using GetUpcomingInstallmentsResponse = Zip.Api.CustomerSummary.Application.Payments.Query.GetUpcomingInstallments.GetUpcomingInstallmentsResponse;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class PaymentsControllerTests : CommonTestsFixture
    {
        private readonly Mock<IMediator> _mediator;

        public PaymentsControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new PaymentsController(null));
        }

        [Fact]
        public async Task Given_PaymentFound_WhenCall_Get_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PaymentDto());

            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.GetAsync("dks");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_PaymentNotFound_WhenCall_Get_ShouldReturn_NotFound()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as PaymentDto);


            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.GetAsync("dks");

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_Get_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception()); 

            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.GetAsync("dks");

            Assert.Equal(500, (result as ObjectResult)?.StatusCode);
        }

        [Fact]
        public async Task Given_InputInvalid_WhenCall_Get_ShouldReturn_BadRequest()
        {
            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.FindAsync(0);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Given_PaymentsFound_WhenCall_Get_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PaymentDto>() { 
                new PaymentDto(),
                new PaymentDto()});

            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.FindAsync(1292);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_PaymentsNotFound_WhenCall_Get_ShouldReturn_NoContent()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as List<PaymentDto>);

            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.FindAsync(1292);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Given_PaymentsError_WhenCall_Get_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentsQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.FindAsync(1292);

            Assert.Equal(500, (result as ObjectResult)?.StatusCode) ;
        }

        [Fact]
        public async Task Given_RefundIdInvalid_ShouldReturn_BadRequest()
        {
            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.RefundAsync(Guid.Empty);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Given_Refunded_Should_Return_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<RefundCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ZipMoney.Services.Payments.Contract.Payments.PaymentRefundResponse());

            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.RefundAsync(Guid.NewGuid());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_RefundError_Should_Return_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<RefundCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.RefundAsync(Guid.NewGuid());

            Assert.Equal(500,  (result as ObjectResult)?.StatusCode);
        }

        [Fact]
        public async Task Given_PayNowSucceed_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<PayNowCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Unit());

            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.PayNowAsync(new PayNowCommand());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_PayNowError_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<PayNowCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.PayNowAsync(new PayNowCommand());

            Assert.Equal(500, (result as ObjectResult)?.StatusCode);
        }
        
        [Fact]
        public async Task Given_Valid_Response_On_GetUpcomingInstallmentsAsync_Should_Return_Ok()
        {
            // Arrange
            var request = Fixture.Create<GetUpcomingInstallmentsQuery>();
            var installments = Fixture.CreateMany<Installment>();
            var response = Fixture.Build<GetUpcomingInstallmentsResponse>()
                                  .With(x => x.Installments, installments)
                                  .Create();
            
            _mediator.Setup(x => x.Send(It.IsAny<GetUpcomingInstallmentsQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(response);

            // Act
            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.GetUpcomingInstallmentsAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResponse = result as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_NullOrEmpty_Response_On_GetUpcomingInstallmentsAsync_Should_Return_NoContent()
        {
            // Arrange
            var request = Fixture.Create<GetUpcomingInstallmentsQuery>();

            _mediator.Setup(x => x.Send(It.IsAny<GetUpcomingInstallmentsQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(null as GetUpcomingInstallmentsResponse);

            // Act
            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.GetUpcomingInstallmentsAsync(request);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var objectResponse = result as ObjectResult;
            objectResponse?.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Given_Exception_Thrown_On_GetUpcomingInstallmentsAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<GetUpcomingInstallmentsQuery>();

            _mediator.Setup(x => x.Send(It.IsAny<GetUpcomingInstallmentsQuery>(), It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new Exception());

            var controller = new PaymentsController(_mediator.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.GetUpcomingInstallmentsAsync(request));
        }

        [Fact]
        public async Task Given_Valid_Response_On_PayOrderAsync_Should_Return_Ok()
        {
            // Arrange
            var request = Fixture.Create<PayOrderCommand>();
            var response = Fixture.Create<PayOrderInnerResponse>();

            _mediator.Setup(x => x.Send(It.IsAny<PayOrderCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(response);

            // Act
            var controller = new PaymentsController(_mediator.Object);
            var result = await controller.PayOrderAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResponse = result as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Exception_Thrown_On_PayOrderAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<PayOrderCommand>();

            _mediator.Setup(x => x.Send(It.IsAny<PayOrderCommand>(), It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new Exception());

            var controller = new PaymentsController(_mediator.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await controller.PayOrderAsync(request));
        }
    }
}
