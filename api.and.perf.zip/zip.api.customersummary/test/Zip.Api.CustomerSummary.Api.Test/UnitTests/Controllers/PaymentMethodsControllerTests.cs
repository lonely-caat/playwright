using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Application.Payments.Command.CreateBankPaymentMethod;
using Zip.Api.CustomerSummary.Application.Payments.Command.SetDefaultPaymentMethod;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetDefaultPaymentMethod;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetLatestPaymentMethods;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethod;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethods;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class PaymentMethodsControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public PaymentMethodsControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new PaymentMethodsController(null));
        }

        [Fact]
        public async Task Given_InvalidConsumerId_WhenCall_Find_ShouldReturn_BadRequest()
        {
            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.FindAsync(-1, string.Empty);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Given_NoPaymentMethodsFound_ShouldReturn_NoContent()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as List<PaymentMethodDto>);

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.FindAsync(2012, string.Empty);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Given_PaymentMethodsFound_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PaymentMethodDto>() { new PaymentMethodDto(), new PaymentMethodDto() });

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.FindAsync(2012, string.Empty);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_Find_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());


            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.FindAsync(2012, string.Empty);

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Given_InvalidConsumerId_WhenCall_FindLatestOnly_ShouldReturn_BadRequest()
        {
            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.FindLatestOnlyAsync(-1, string.Empty);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Given_NoLatestPaymentMethodsFound_ShouldReturn_NoContent()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetLatestPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as List<PaymentMethodDto>);

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.FindLatestOnlyAsync(2012, string.Empty);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Given_LatestPaymentMethodsFound_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetLatestPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PaymentMethodDto>() { new PaymentMethodDto(), new PaymentMethodDto() });

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.FindLatestOnlyAsync(2012, string.Empty);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_FindLatestOnly_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetLatestPaymentMethodsQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());


            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.FindLatestOnlyAsync(2012, string.Empty);

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Given_InvalidId_WhenCall_Get_ShouldReturn_BadRequest()
        {
            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.GetAsync(Guid.Empty);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Given_NoPaymentMethodFound_ShouldReturn_NotFound()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as PaymentMethodDto);

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.GetAsync(Guid.NewGuid());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Given_PaymentMethodFound_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PaymentMethodDto());

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.GetAsync(Guid.NewGuid());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_Get_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPaymentMethodQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());


            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.GetAsync(Guid.NewGuid());

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Given_CreatedBankPaymentMethod_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<CreateBankPaymentMethodCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ZipMoney.Services.Payments.Contract.PaymentMethods.PaymentMethodResponse());

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.CreateBankPaymentMethodAsync(new CreateBankPaymentMethodCommand());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_ErrorCreatedBankPaymentMethod_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<CreateBankPaymentMethodCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());


            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.CreateBankPaymentMethodAsync(new CreateBankPaymentMethodCommand());

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }
        
        [Fact]
        public async Task Given_InvalidConsumerId_WhenCall_GetDefaultPaymentMethod_ShouldReturn_BadRequest()
        {
            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.GetDefaultPaymentMethodAsync(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public async Task Given_NoDefaultPaymentMethodFound_ShouldReturn_NotFound()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetDefaultPaymentMethodQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as PaymentMethodDto);

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.GetDefaultPaymentMethodAsync(100);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Given_DefaultPaymentMethodFound_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetDefaultPaymentMethodQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PaymentMethodDto());

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.GetDefaultPaymentMethodAsync(3928);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_ErrorGetDefault_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetDefaultPaymentMethodQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.GetDefaultPaymentMethodAsync(3928);

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Given_SetDefaultPaymentMethod_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<SetDefaultPaymentMethodCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.SetDefaultPaymentMethodAsync(new SetDefaultPaymentMethodCommand());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Given_ErrorSetDefault_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<SetDefaultPaymentMethodCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new PaymentMethodsController(_mediator.Object);
            var result = await controller.SetDefaultPaymentMethodAsync(new SetDefaultPaymentMethodCommand());

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }
    }
}
