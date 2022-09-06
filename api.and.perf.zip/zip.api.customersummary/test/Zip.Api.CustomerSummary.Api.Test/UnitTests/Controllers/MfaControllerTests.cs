using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Refit;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Mfa.Query;
using Zip.Api.CustomerSummary.Domain.Entities.Mfa;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class MfaControllerTests : CommonTestsFixture
    {
        private readonly Mock<ILogger<MfaController>> _logger;

        private readonly MfaController _target;
        
        public MfaControllerTests()
        {
            _logger = new Mock<ILogger<MfaController>>();

            _target = new MfaController(_logger.Object, MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Input_On_GetMfaSmsDataAsync_Should_Return_200()
        {
            // Arrange
            var consumerId = 1234;
            var response = Fixture.Create<MfaSmsDataResponse>();
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetMfaSmsDataQuery>(), default))
                .ReturnsAsync(response);

            // Act
            var actual = await _target.GetMfaSmsDataAsync(consumerId);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Valid_Input_On_GetMfaSmsDataAsync_And_NotFound_In_Proxy_Should_Return_204()
        {
            // Arrange
            var consumerId = 1234;
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetMfaSmsDataQuery>(), default))
                .ReturnsAsync((MfaSmsDataResponse)null);

            // Act
            var actual = await _target.GetMfaSmsDataAsync(consumerId);

            // Assert
            actual.Should().BeOfType<StatusCodeResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Given_Refit_ApiException_On_GetMfaSmsDataAsync_Should_Throw_To_Middleware()
        {
            // Arrange
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetMfaSmsDataQuery>(), default))
                .ThrowsAsync(new MfaApiException());

            // Act & Assert
            await Assert.ThrowsAsync<MfaApiException>(async () =>
                await _target.GetMfaSmsDataAsync(Fixture.Create<long>())
            );
        }

        [Fact]
        public async Task Given_Exception_On_GetMfaSmsDataAsync_Should_Return_500()
        {
            // Arrange
            var consumerId = 1234;
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetMfaSmsDataQuery>(), default))
                .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetMfaSmsDataAsync(consumerId);

            // Assert
            actual.Should().BeOfType<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }
    }
}