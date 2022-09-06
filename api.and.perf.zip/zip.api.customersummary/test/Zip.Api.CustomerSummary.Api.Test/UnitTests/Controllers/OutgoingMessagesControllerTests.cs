using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendPaidOutAndClosedEmail;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendPayNowLink;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class OutgoingMessagesControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public OutgoingMessagesControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new OutgoingMessagesController(null));
        }

        [Fact]
        public async Task Given_SentPayNowLinkAsync_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<SendPayNowLinkCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var controller = new OutgoingMessagesController(_mediator.Object);
            var result = await controller.SendPayNowLinkAsync(new SendPayNowLinkCommand());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Given_ErrorSent_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<SendPayNowLinkCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new OutgoingMessagesController(_mediator.Object);
            var result = await controller.SendPayNowLinkAsync(new SendPayNowLinkCommand());

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Given_SentPaidOut_Close_ShouldReturn_200()
        {
            _mediator.Setup(x => x.Send(It.IsAny<SendPaidOutAndClosedEmailCommand>(), default))
                            .ReturnsAsync(true);

            var controller = new OutgoingMessagesController(_mediator.Object);
            dynamic result = await controller.SendPaidOutAndCloseEmailAsync(1245878);

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Given_SentPaidOut_Close_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<SendPaidOutAndClosedEmailCommand>(), default))
                            .ThrowsAsync(new Exception());

            var controller = new OutgoingMessagesController(_mediator.Object);
            dynamic result = await controller.SendPaidOutAndCloseEmailAsync(1245878);

            Assert.Equal(500, result.StatusCode);
        }
    }
}
