using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.UnitTests.Common;
using Zip.Api.CustomerSummary.Domain.Entities.GoogleAddress;
using Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService.Interfaces;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Common.Constants;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class AddressControllerTests : CommonTestFixture
    {
        private readonly Mock<IAddressSearch> addressSearch = new Mock<IAddressSearch>();
        private AddressController GetController() => new AddressController(addressSearch.Object);

        [Fact]
        public void Given_NullAddressSearch_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new AddressController(null);
            });
        }

        [Fact]
        public async Task Given_NullInput_WhenCall_Search_ShouldReturn_BadRequest()
        {
            var controller = GetController();
            var response = await controller.SearchAsync(null, Regions.Australia);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Given_NotFoundPredictions_ShouldReturn_204()
        {
            addressSearch.Setup(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(Enumerable.Empty<Prediction>());

            var controller = GetController();
            var response = await controller.SearchAsync("test", "test");

            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task Given_FoundPredictions_ShouldReturn_Predictions()
        {
            addressSearch.Setup(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new List<Prediction> { new Prediction() { }, new Prediction() { } });
            var controller = GetController();
            var response = await controller.SearchAsync("test", "test") as OkObjectResult;

            Assert.Equal(2, ((response?.Value as IEnumerable<Prediction>) ?? Array.Empty<Prediction>()).Count());
        }

        [Fact]
        public async Task Given_Exception_SearchAsync_ShouldReturn_InternalServerError()
        {
            // Arrange
            addressSearch.Setup(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            // Act
            var controller = GetController();
            var response = (ObjectResult) await controller.SearchAsync("test", "test");

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Given_InvalidInput_VerifyAsync_ShouldReturn_BadRequest()
        {
            // Act
            var controller = GetController();
            var response = await controller.VerifyAsync(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Given_NullResponse_VerifyAsync_ShouldReturn_NotFoundError()
        {
            // Arrange
            addressSearch.Setup(x => x.FindAndVerifyAsync(It.IsAny<string>())).ReturnsAsync((GoogleAddress)null);

            // Act
            var controller = GetController();
            var response = await controller.VerifyAsync("test");

            // Assert
            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task Given_ValidResponse_VerifyAsync_ShouldReturn_Result()
        {
            // Arrange
            var mockResponse = _fixture.Create<GoogleAddress>();
            addressSearch.Setup(x => x.FindAndVerifyAsync(It.IsAny<string>())).ReturnsAsync(mockResponse);

            // Act
            var controller = GetController();
            var response = (OkObjectResult)await controller.VerifyAsync("test");

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);

            var actualResponseModel = (GoogleAddress)response.Value;
            Assert.Equal<GoogleAddress>(mockResponse, actualResponseModel);
        }

        [Fact]
        public async Task Given_Exception_VerifyAsync_ShouldReturn_InternalServerError()
        {
            // Arrange
            var mockResponse = _fixture.Create<GoogleAddress>();
            addressSearch.Setup(x => x.FindAndVerifyAsync(It.IsAny<string>())).ThrowsAsync(new Exception());

            // Act
            var controller = GetController();
            var response = (ObjectResult)await controller.VerifyAsync("test");

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }
    }
}
