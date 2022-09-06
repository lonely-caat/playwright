using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateLoginStatus;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdateLoginStatusCommandHandlerTest : CommonTestsFixture
    {
        private readonly UpdateLoginStatusCommandHandler _target;

        private readonly Mock<ILogger<UpdateLoginStatusCommandHandler>> _logger;

        private readonly Mock<IMediator> _mediator;

        private readonly Mock<ICustomerCoreService> _customerCoreService;

        public UpdateLoginStatusCommandHandlerTest()
        {
            _logger = new Mock<ILogger<UpdateLoginStatusCommandHandler>>();

            _mediator = new Mock<IMediator>();

            _customerCoreService = new Mock<ICustomerCoreService>();

            _target = new UpdateLoginStatusCommandHandler(_logger.Object, _mediator.Object, _customerCoreService.Object);
        }

        [Fact]
        public async Task Given_ValidEnableLoginRequest_Handle_Should_Return_UpdateLoginStatusResponse()
        {
            // Arrange
            var request = Fixture.Build<UpdateLoginStatusCommand>()
                                 .With(x => x.ChangedBy, "johnny.vuong@zip.co")
                                 .With(x => x.LoginStatusType, LoginStatusType.Enabled)
                                 .Create();

            var customerCoreServiceResponse = Fixture.Create<UpdateLoginStatusResponse>();

            var mockContractQueryResponse = Fixture.Create<ContactDto>();

            _mediator.Setup(x => x.Send(
                                It.IsAny<GetContactQuery>(),
                                It.IsAny<CancellationToken>()))
                     .ReturnsAsync(mockContractQueryResponse);

            _customerCoreService.Setup(x => x.EnableCustomerLoginAsync(
                                           It.IsAny<UpdateLoginStatusRequest>(),
                                           It.IsAny<CancellationToken>()))
                                .ReturnsAsync(customerCoreServiceResponse);

            _customerCoreService.Setup(x => x.DisableCustomerLoginAsync(
                                           It.IsAny<UpdateLoginStatusRequest>(),
                                           It.IsAny<CancellationToken>()))
                                .ReturnsAsync(customerCoreServiceResponse);

            // Act
            var response = await _target.Handle(request, default);

            // Assert
            Assert.Equal(customerCoreServiceResponse.IsSuccess, response.IsSuccess);
            Assert.Equal(customerCoreServiceResponse.Status, response.Status);

            _mediator.Verify(x => x.Send(
                                It.Is<GetContactQuery>(y => y.ConsumerId == request.ConsumerId),
                                default), Times.Once);

            _customerCoreService.Verify(x => x.EnableCustomerLoginAsync(
                                            It.Is<UpdateLoginStatusRequest>(y => y.Email == mockContractQueryResponse.Email &&
                                                                           y.CreatedBy == request.ChangedBy),
                                            default), Times.Once);
        }

        [Fact]
        public async Task Given_ValidDisableLoginRequest_Handle_Should_Return_UpdateLoginStatusResponse()
        {
            // Arrange
            var request = Fixture.Build<UpdateLoginStatusCommand>()
                                 .With(x => x.LoginStatusType, LoginStatusType.Disabled)
                                 .Create();

            var mockContractQueryResponse = Fixture.Create<ContactDto>();

            _mediator.Setup(x => x.Send(
                                It.IsAny<GetContactQuery>(),
                                It.IsAny<CancellationToken>()))
                     .ReturnsAsync(mockContractQueryResponse);

            var customerCoreServiceResponse = Fixture.Create<UpdateLoginStatusResponse>();

            _customerCoreService.Setup(x => x.DisableCustomerLoginAsync(
                                           It.IsAny<UpdateLoginStatusRequest>(),
                                           It.IsAny<CancellationToken>()))
                                .ReturnsAsync(customerCoreServiceResponse);

            // Act
            var response = await _target.Handle(request, default);

            // Assert
            Assert.Equal(customerCoreServiceResponse.IsSuccess, response.IsSuccess);
            Assert.Equal(customerCoreServiceResponse.Status, response.Status);

            _customerCoreService.Verify(x => x.DisableCustomerLoginAsync(
                                            It.Is<UpdateLoginStatusRequest>(y => y.Email == mockContractQueryResponse.Email &&
                                                                                y.CreatedBy == request.ChangedBy),
                                            default), Times.Once);
        }

        [Fact]
        public async Task Given_InvalidResetRequiredLoginRequest_Handle_Should_Return_UpdateLoginStatusUnprocessableException()
        {
            // Arrange
            var request = Fixture.Build<UpdateLoginStatusCommand>()
                                 .With(x => x.LoginStatusType, LoginStatusType.ResetRequired)
                                 .Create();

            var mockContractQueryResponse = Fixture.Create<ContactDto>();

            _mediator.Setup(x => x.Send(
                                It.IsAny<GetContactQuery>(),
                                It.IsAny<CancellationToken>()))
                     .ReturnsAsync(mockContractQueryResponse);

            // Act & Assert
            await Assert.ThrowsAsync<CustomerCoreApiException>(
                async () => await _target.Handle(request, default));
        }

        [Theory]
        [InlineData(LoginStatusType.Disabled)]
        [InlineData(LoginStatusType.Enabled)]
        public async Task Given_InvalidCustomerCoreApi_Handle_Should_Return_CustomerCoreApiException(LoginStatusType status)
        {
            // Arrange
            var request = Fixture.Build<UpdateLoginStatusCommand>()
                                 .With(x => x.LoginStatusType, status)
                                 .Create();
            var mockContractQueryResponse = Fixture.Create<ContactDto>();

            _mediator.Setup(x => x.Send(
                                It.IsAny<GetContactQuery>(),
                                It.IsAny<CancellationToken>()))
                     .ReturnsAsync(mockContractQueryResponse);

            _customerCoreService.Setup(x => x.EnableCustomerLoginAsync(
                                           It.IsAny<UpdateLoginStatusRequest>(),
                                           It.IsAny<CancellationToken>()))
                                .ThrowsAsync(new CustomerCoreApiException());

            _customerCoreService.Setup(x => x.DisableCustomerLoginAsync(
                                           It.IsAny<UpdateLoginStatusRequest>(),
                                           It.IsAny<CancellationToken>()))
                                .ThrowsAsync(new CustomerCoreApiException());

            // Act & Assert
            await Assert.ThrowsAsync<CustomerCoreApiException>(
                async () => await _target.Handle(request, default));
        }

        [Fact]
        public async Task Given_InvalidContactModel_Handle_Should_Return_ContactNotFoundException()
        {
            // Arrange
            var request = Fixture.Create<UpdateLoginStatusCommand>();
            var mockContractQueryResponse = new ContactDto();

            _mediator.Setup(x => x.Send(
                                It.IsAny<GetContactQuery>(),
                                It.IsAny<CancellationToken>()))
                     .ReturnsAsync(mockContractQueryResponse);

            // Act & Assert
            await Assert.ThrowsAsync<ContactNotFoundException>(
                async () => await _target.Handle(request, default));
        }
    }
}
