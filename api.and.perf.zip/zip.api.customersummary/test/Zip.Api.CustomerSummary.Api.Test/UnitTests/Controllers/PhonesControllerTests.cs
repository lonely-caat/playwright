using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdatePhoneStatus;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetPhoneHistory;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using AutoFixture;
using Zip.Api.CustomerSummary.Api.Test.UnitTests.Common;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class PhonesControllerTests : CommonTestFixture
    {
        private readonly Mock<IMediator> _mediator;

        public PhonesControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullEx()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PhonesController(null);
            });
        }

        [Fact]
        public async Task Given_InvalidConsumerId_ShouldReturn_BadRequest()
        {
            var controller = new PhonesController(_mediator.Object);
            var result = await controller.GetConsumerPhoneHistoryAsync(-20);

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Given_PhoneNumbersFound_ShouldReturn()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetPhoneHistoryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Phone>
                {
                    new Phone{},
                    new Phone()
                });

            var controller = new PhonesController(_mediator.Object);
            var result = await controller.GetConsumerPhoneHistoryAsync(2929);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_PhoneNumbersFound_ShouldReturn_InternalServerError()
        {
            // Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetPhoneHistoryQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            
            // Act
            var controller = new PhonesController(_mediator.Object);
            var result = (ObjectResult) await controller.GetConsumerPhoneHistoryAsync(2929);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task Given_ValidPhoneUpdate_UpdatePhoneStatusAsync_ShouldReturn_Result()
        {
            // Arrange
            var mockResponse = _fixture.Create<Phone>();
            _mediator.Setup(x => x.Send(It.IsAny<UpdatePhoneStatusCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse);
            var mockInput = _fixture.Create<UpdatePhoneStatusCommand>();

            // Act
            var controller = new PhonesController(_mediator.Object);
            var result = (OkObjectResult) await controller.UpdatePhoneStatusAsync(mockInput);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

            var actualResponseModel = (Phone)result.Value;
            Assert.Equal<Phone>(mockResponse, actualResponseModel);

        }

        [Fact]
        public async Task Given_InvalidPhoneUpdate_UpdatePhoneStatusAsync_ShouldReturn_InternalServerError()
        {
            // Arrange
            _mediator.Setup(x => x.Send(It.IsAny<UpdatePhoneStatusCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            var mockInput = _fixture.Create<UpdatePhoneStatusCommand>();

            // Act
            var controller = new PhonesController(_mediator.Object);
            var result = (ObjectResult)await controller.UpdatePhoneStatusAsync(mockInput);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
