using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.UnitTests.Common;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V2;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class ConsumersV2ControllerTests : CommonTestFixture
    {
        private readonly Mock<IMediator> _mediator;
        private readonly ConsumersV2Controller _target;
        public ConsumersV2ControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _target = new ConsumersV2Controller(_mediator.Object);
        }

        [Fact]
        public async Task Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ConsumersV2Controller(null));
        }

        [Fact]
        public async Task Given_valid_Input_Return_200()
        {
            // Arrange
            var mockInput = _fixture.Create<GetConsumerQueryV2>();
            var mockResponse = _fixture.Create<Consumer>();
            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQueryV2>(), CancellationToken.None)).ReturnsAsync(mockResponse);

            // Act
            var response = (OkObjectResult) await _target.GetAsync(mockInput);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            
            var actualResponseModel = (Consumer)response.Value;
            Assert.Equal<Consumer>(mockResponse, actualResponseModel);
        }

        [Fact]
        public async Task Given_Null_Response_Returns_NotFoundError()
        {
            // Arrange
            var mockInput = _fixture.Create<GetConsumerQueryV2>();
            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQueryV2>(), CancellationToken.None)).ReturnsAsync((Consumer)null);

            // Act
            var result = await _target.GetAsync(mockInput);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Given_Null_Response_Returns_InternalServerError()
        {
            // Arrange
            var mockInput = _fixture.Create<GetConsumerQueryV2>();
            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQueryV2>(), CancellationToken.None)).ThrowsAsync(new Exception());

            // Act
            var result = (ObjectResult) await _target.GetAsync(mockInput);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }


    }
}
