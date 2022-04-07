using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendResetPasswordEmailNew;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateContact;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class ContactsControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public ContactsControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task Given_InvalidConsumerId_WhenCall_Get_ShouldReturn_BadRequest()
        {
            var controller = new ContactsController(_mediator.Object);
            var result = await controller.GetAsync(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task Given_NoContactFound_ShouldReturn_NotFound()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as ContactDto);

            var controller = new ContactsController(_mediator.Object);
            var result = await controller.GetAsync(100);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Given_ContactFound_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContactDto());

            var controller = new ContactsController(_mediator.Object);
            var result = await controller.GetAsync(100);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_Get_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());


            var controller = new ContactsController(_mediator.Object);
            var result = await controller.GetAsync(2012);

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Given_Updated_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<UpdateContactCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var controller = new ContactsController(_mediator.Object);
            var result = await controller.UpdateAsync(new UpdateContactCommand());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_Update_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<UpdateContactCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new ContactsController(_mediator.Object);
            var result = await controller.UpdateAsync(new UpdateContactCommand());

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Given_ResetPassword_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<SendResetPasswordEmailNewCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var controller = new ContactsController(_mediator.Object);
            var result = await controller.ResetPasswordAsync(new SendResetPasswordEmailNewCommand());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_ResetPassword_ShouldReturn_424()
        {
            _mediator.Setup(x => x.Send(It.IsAny<SendResetPasswordEmailNewCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new CoreApiException());

            var controller = new ContactsController(_mediator.Object);

            await Assert.ThrowsAsync<CoreApiException>(
                async () => await controller.ResetPasswordAsync(new SendResetPasswordEmailNewCommand()));
        }
    }
}
