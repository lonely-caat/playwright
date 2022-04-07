using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;
using Zip.Core.Extensions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class CustomerCoreServiceTests : CommonTestsFixture
    {
        private readonly CustomerCoreService _target;

        private readonly Mock<ILogger<CustomerCoreService>> _logger;

        private readonly Mock<ICustomerCoreServiceProxy> _customerCoreServiceProxy;

        private const string email = "johnny.vuong@zip.co";

        public CustomerCoreServiceTests()
        {
            _logger = new Mock<ILogger<CustomerCoreService>>();
            _customerCoreServiceProxy = new Mock<ICustomerCoreServiceProxy>();
            _target = new CustomerCoreService(_logger.Object, _customerCoreServiceProxy.Object);
        }

        [Fact]
        public async Task Given_ValidEmail_GetLoggingStatusAsync_Should_Return_LoginStatusResponse()
        {
            // Arrange
            var mockGetLoginStatusRequest = Fixture.Create<GetLoginStatusRequest>();
            var mockGetLoginStatusResponse = Fixture.Create<GetLoginStatusResponse>();
            var mockResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(mockGetLoginStatusResponse.ToJsonString())};
            
            _customerCoreServiceProxy.Setup(
                x => x.GetLoginStatusAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse);

            // Act
            var response = await _target.GetCustomerLoginStatusAsync(mockGetLoginStatusRequest, default);

            // Assert
            Assert.Equal(mockGetLoginStatusResponse.Data.LoginStatus, response.Data.LoginStatus);
            _customerCoreServiceProxy.Verify(x => x.GetLoginStatusAsync(mockGetLoginStatusRequest.Email, default), Times.Once);
        }

        [Fact]
        public async Task Given_NotFoundError_GetLoggingStatusAsync_Should_Return_CustomerCoreApiException()
        {
            // Arrange
            var mockGetloginStatusRequest = Fixture.Create<GetLoginStatusRequest>();
            var mockResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound};

            _customerCoreServiceProxy.Setup(
                x => x.GetLoginStatusAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse);

            // Act & Assert
            await Assert.ThrowsAsync<CustomerCoreApiException>(
                async () => await _target.GetCustomerLoginStatusAsync(mockGetloginStatusRequest, default));
        }

        [Fact]
        public async Task Given_Exception_GetLoggingStatusAsync_Should_Return_CustomerCoreApiException()
        {
            // Arrange
            var mockGetloginStatusRequest = Fixture.Create<GetLoginStatusRequest>();
            _customerCoreServiceProxy.Setup(
                x => x.GetLoginStatusAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<CustomerCoreApiException>(
                async () => await _target.GetCustomerLoginStatusAsync(mockGetloginStatusRequest, default));
        }

        [Fact]
        public async Task Given_Valid_PostDisableLoginAsyncRequest_Should_Return_UpdateLoginStatusResponse()
        {
            // Arrange
            var mockUpdateLoginStatusResponse = Fixture.Create<GetLoginStatusResponse>();
            var mockResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(mockUpdateLoginStatusResponse.ToJsonString()) };
            var request = Fixture.Create<UpdateLoginStatusRequest>();

            var output = Fixture.Build<UpdateLoginStatusResponse>()
                                .With(x => x.IsSuccess, mockUpdateLoginStatusResponse.IsSuccess)
                                .With(x => x.Status, mockUpdateLoginStatusResponse.Status)
                                .Create();
            
            _customerCoreServiceProxy.Setup(
                x => x.PostDisableLoginAsync(It.IsAny<UpdateLoginStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse);

            // Act
            var response = await _target.DisableCustomerLoginAsync(request, default);

            // Assert
            Assert.Equal(output.IsSuccess, response.IsSuccess);
            Assert.Equal(output.Status, response.Status);
            _customerCoreServiceProxy.Verify(x => x.PostDisableLoginAsync(request, default), Times.Once);
        }

        [Fact]
        public async Task Given_Exception_PostDisableLoginAsyncRequest_Should_Return_CustomerCoreApiException()
        {
            // Arrange
            var request = Fixture.Create<UpdateLoginStatusRequest>();

            _customerCoreServiceProxy.Setup(
                x => x.PostDisableLoginAsync(It.IsAny<UpdateLoginStatusRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<CustomerCoreApiException>(
                async () => await _target.DisableCustomerLoginAsync(request, default));
        }

        [Fact]
        public async Task Given_NonSuccessfulResponse_PostDisableLoginAsyncRequest_Should_Return_CustomerCoreApiException()
        {
            // Arrange
            var mockResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError };
            var request = Fixture.Create<UpdateLoginStatusRequest>();

            _customerCoreServiceProxy.Setup(
                x => x.PostDisableLoginAsync(It.IsAny<UpdateLoginStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse);

            // Act & Assert
            await Assert.ThrowsAsync<CustomerCoreApiException>(
                async () => await _target.DisableCustomerLoginAsync(request, default));
        }

        [Fact]
        public async Task Given_Valid_PostEnableLoginAsyncRequest_Should_Return_HttpResponseMessage()
        {
            // Arrange
            var mockUpdateLoginStatusResponse = Fixture.Create<GetLoginStatusResponse>();
            var mockResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(mockUpdateLoginStatusResponse.ToJsonString()) };
            var request = Fixture.Create<UpdateLoginStatusRequest>();

            var output = Fixture.Build<UpdateLoginStatusResponse>()
                                .With(x => x.IsSuccess, mockUpdateLoginStatusResponse.IsSuccess)
                                .With(x => x.Status, mockUpdateLoginStatusResponse.Status)
                                .Create();

            _customerCoreServiceProxy.Setup(
                x => x.PostEnableLoginAsync(It.IsAny<UpdateLoginStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse);

            // Act
            var response = await _target.EnableCustomerLoginAsync(request, default);

            // Assert
            Assert.Equal(output.IsSuccess, response.IsSuccess);
            Assert.Equal(output.Status, response.Status);
            _customerCoreServiceProxy.Verify(x => x.PostEnableLoginAsync(request, default), Times.Once);
        }

        [Fact]
        public async Task Given_Exception_PostEnableLoginAsyncRequest_Should_Return_CustomerCoreApiException()
        {
            // Arrange
            var request = Fixture.Create<UpdateLoginStatusRequest>();

            _customerCoreServiceProxy.Setup(
                x => x.PostEnableLoginAsync(It.IsAny<UpdateLoginStatusRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<CustomerCoreApiException>(
                async () => await _target.EnableCustomerLoginAsync(request, default));
        }

        [Fact]
        public async Task Given_NonSuccessfulResponse_PostEnableLoginAsyncRequest_Should_Return_CustomerCoreApiException()
        {
            // Arrange
            var mockResponse = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
            var request = Fixture.Create<UpdateLoginStatusRequest>();

            _customerCoreServiceProxy.Setup(
                x => x.PostEnableLoginAsync(It.IsAny<UpdateLoginStatusRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResponse);

            // Act & Assert
            await Assert.ThrowsAsync<CustomerCoreApiException>(
                async () => await _target.EnableCustomerLoginAsync(request, default));
        }
    }
}