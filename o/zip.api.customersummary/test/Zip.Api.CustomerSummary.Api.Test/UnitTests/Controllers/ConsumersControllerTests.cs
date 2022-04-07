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
using Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile.Models;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateConsumer;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateLoginStatus;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.Vcn;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetLoginStatus;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class ConsumersControllerTests : CommonTestFixture
    {
        private readonly Mock<IMediator> mockMediator = new Mock<IMediator>();

        private ConsumersController GetController() => new ConsumersController(mockMediator.Object);

        [Fact]
        public void Given_NullMediator_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { new ConsumersController(null); });
        }


        [Fact]
        public async Task Given_InvalidConsumerId_WhenCall_GetAsync_ShouldReturn_BadRequest()
        {
            var controller = GetController();

            var response = await controller.GetAsync(-1);

            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Given_ValidConsumerId_But_NoContent_WhenCall_GetAsync_ShouldReturn_NotFound()
        {
            mockMediator.Setup(x =>
                                   x.Send<Consumer>(
                                       It.IsAny<GetConsumerQuery>(),
                                       It.IsAny<CancellationToken>()))
                        .ReturnsAsync(null as Consumer)
                        .Verifiable("failed");

            var controller = GetController();

            var response = await controller.GetAsync(11234);

            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task Given_InvalidConsumerId_GetAsync_ShouldReturn_InternalServerError()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<GetConsumerQuery>(),
                                   CancellationToken.None))
                        .ThrowsAsync(new Exception());

            // Act
            var controller = GetController();
            var response = (ObjectResult)await controller.GetAsync(11234);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Given_ValidConsumerId_WhenCall_GetAsync_ShouldReturn_Result()
        {
            mockMediator.Setup(x =>
                                   x.Send<Consumer>(
                                       It.IsAny<GetConsumerQuery>(),
                                       It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new Consumer())
                        .Verifiable("failed");

            var controller = GetController();

            var response = await controller.GetAsync(11234);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task
            Given_InvalidCustomerIdAndProduct_GetAsyncByCustomerIdAndProduct_ShouldReturn_InternalServerError()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<GetConsumerForVcnQuery>(),
                                   CancellationToken.None))
                        .ThrowsAsync(new Exception());

            // Act
            var controller = GetController();
            var response = (ObjectResult)await controller.GetConsumerForVcnAsync(new GetConsumerForVcnQuery());

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Given_ValidCustomerIdAndProduct_WhenCall_GetConsumerForVcnAsync_ShouldReturn_Result()
        {
            mockMediator.Setup(x =>
                                   x.Send<Consumer>(
                                       It.IsAny<GetConsumerForVcnQuery>(),
                                       It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new Consumer())
                        .Verifiable("failed");

            var controller = GetController();

            var response =
                await controller.GetConsumerForVcnAsync(
                    new GetConsumerForVcnQuery(Guid.NewGuid(), ProductClassification.zipPay.ToString()));

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Given_VaildPayload_UpdateAsync_ShouldReturn_Result()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<UpdateConsumerCommand>(),
                                   CancellationToken.None))
                        .ReturnsAsync(Unit.Value);

            // Act
            var controller = GetController();
            var response = await controller.UpdateAsync(new UpdateConsumerCommand());

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task Given_InvalidPayload_UpdateAsync_ShouldReturn_InternalServerError()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<UpdateConsumerCommand>(),
                                   CancellationToken.None))
                        .ThrowsAsync(new Exception());

            // Act
            var controller = GetController();
            var response = (ObjectResult)await controller.UpdateAsync(new UpdateConsumerCommand());

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Given_ValidPayload_CloseAccountAsync_ShouldReturn_Result()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<CloseAccountCommand>(),
                                   CancellationToken.None))
                        .ReturnsAsync(Unit.Value);

            // Act
            var controller = GetController();
            var response = await controller.CloseAccountAsync(new CloseAccountCommand());

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task Given_InvalidPayload_CloseAccountAsync_ShouldReturn_InternalServerError()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<CloseAccountCommand>(),
                                   CancellationToken.None))
                        .ThrowsAsync(new Exception());

            // Act
            var controller = GetController();
            var response = (ObjectResult)await controller.CloseAccountAsync(new CloseAccountCommand());

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Given_InvalidPayload_CloseAccountAsync_ShouldReturn_CloseAccountUnprocessableException()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<CloseAccountCommand>(),
                                   CancellationToken.None))
                        .ThrowsAsync(new CloseAccountUnprocessableException());

            // Act
            var controller = GetController();
            var response = (ObjectResult)await controller.CloseAccountAsync(new CloseAccountCommand());

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Given_ValidPayload_GetCloseAccountCreditProfileAsync_ShouldReturn_Result()
        {
            // Arrange
            var mockInput = _fixture.Create<GetCloseAccountCreditProfileQueryResult>();
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<GetCloseAccountCreditProfileQuery>(),
                                   CancellationToken.None))
                        .ReturnsAsync(mockInput);

            // Act
            var controller = GetController();
            var response = await controller.GetCloseAccountCreditProfileAsync(1);

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Given_InvalidPayload_GetCloseAccountCreditProfileAsync_ShouldReturn_InternalServerError()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<GetCloseAccountCreditProfileQuery>(),
                                   CancellationToken.None))
                        .ThrowsAsync(new Exception());

            // Act
            var controller = GetController();
            var response = (ObjectResult)await controller.GetCloseAccountCreditProfileAsync(1);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Given_InvalidPayload_GetCloseAccountCreditProfileAsync_ShouldReturn_BadRequest()
        {
            // Act
            var controller = GetController();
            var response = (ObjectResult)await controller.GetCloseAccountCreditProfileAsync(-1);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Given_InvalidPayload_GetAccountInfoAsync_ShouldReturn_BadRequest()
        {
            // Act
            var controller = GetController();
            var response = await controller.GetAccountInfoAsync(-1);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Given_InvalidPayload_GetAccountInfoAsync_ShouldReturn_InternalServerError()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<GetAccountInfoQuery>(),
                                   CancellationToken.None))
                        .ThrowsAsync(new Exception());

            // Act
            var controller = GetController();
            var response = (ObjectResult)await controller.GetAccountInfoAsync(1);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Given_InvalidPayload_GetAccountInfoAsync_ShouldReturn_BadRequest_When_An_Exception_Occurs()
        {
            // Arrange
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<GetAccountInfoQuery>(),
                                   CancellationToken.None))
                        .ThrowsAsync(new ArgumentException());

            // Act
            var controller = GetController();
            var response = await controller.GetAccountInfoAsync(1);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async Task Given_ValidPayload_GetAccountInfoAsync_ShouldReturn_Results()
        {
            // Arrange
            var mockResponse = _fixture.Create<GetAccountInfoQueryResult>();
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<GetAccountInfoQuery>(),
                                   CancellationToken.None))
                        .ReturnsAsync(mockResponse);

            // Act
            var controller = GetController();
            var response = await controller.GetAccountInfoAsync(1);

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Given_ValidPayload_GetLoginStatus_ShouldReturn_Results()
        {
            // Arrange
            var mockResponse = _fixture.Create<LoginStatusResponse>();
            var mockRequest = _fixture.Create<GetLoginStatusQuery>();
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<GetLoginStatusQuery>(),
                                   CancellationToken.None))
                        .ReturnsAsync(mockResponse);

            // Act
            var controller = GetController();
            var response = await controller.GetLoginStatusAsync(mockRequest);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, (response as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Given_ValidPayload_UpdateLoginStatus_ShouldReturn_Results()
        {
            // Arrange
            var mockRequest = _fixture.Create<UpdateLoginStatusCommand>();
            mockMediator.Setup(x => x.Send(
                                   It.IsAny<UpdateLoginStatusCommand>(),
                                   CancellationToken.None));

            // Act
            var controller = GetController();
            var response = await controller.UpdateLoginStatusAsync(mockRequest);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, (response as OkObjectResult).StatusCode);
        }
    }
}