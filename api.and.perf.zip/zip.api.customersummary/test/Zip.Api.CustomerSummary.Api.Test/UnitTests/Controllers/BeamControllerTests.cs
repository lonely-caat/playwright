using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Beam.Command.CreateReconciliationReport;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetCustomerDetails;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetRewardActivity;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetTransactionRewardDetails;
using Zip.Api.CustomerSummary.Application.Beam.Query.PollReconciliationReport;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class BeamControllerTests : CommonTestsFixture
    {
        private readonly Mock<ILogger<BeamController>> _logger;

        private readonly BeamController _target;

        public BeamControllerTests()
        {
            _logger = new Mock<ILogger<BeamController>>();

            _target = new BeamController(_logger.Object, MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Input_On_GetCustomerDetailsAsync_Should_Return_200()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var response = Fixture.Create<CustomerDetails>();
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetCustomerDetailsQuery>(), default))
                .ReturnsAsync(response);

            // Act
            var actual = await _target.GetCustomerDetailsAsync(customerId);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Null_Result_On_GetCustomerDetailsAsync_Should_Return_204()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetCustomerDetailsQuery>(), default))
                .ReturnsAsync(null as CustomerDetails);

            // Act
            var actual = await _target.GetCustomerDetailsAsync(customerId);

            // Assert
            actual.Should().BeOfType<NoContentResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Given_BeamApiException_On_GetCustomerDetailsAsync_Should_Throw_To_Middleware()
        {
            // Arrange
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetCustomerDetailsQuery>(), default))
                .ThrowsAsync(new BeamApiException());

            // Act & Assert
            await Assert.ThrowsAsync<BeamApiException>(async () =>
                await _target.GetCustomerDetailsAsync(Fixture.Create<Guid>())
            );
        }

        [Fact]
        public async Task Given_Exception_On_GetCustomerDetailsAsync_Should_Return_500()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetCustomerDetailsQuery>(), default))
                .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetCustomerDetailsAsync(customerId);

            // Assert
            actual.Should().BeOfType<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_On_CreateReconciliationReportAsync_Should_Return_202Accepted()
        {
            // Arrange
            var uuid = Guid.NewGuid();
            var response = Fixture.Build<CreateReconciliationReportResponse>()
                            .With(x => x.Uuid, uuid)
                            .Create();

            MockMediator
                .Setup(x => x.Send(It.IsAny<CreateReconciliationReportCommand>(), default))
                .ReturnsAsync(response);

            // Act
            var actual = await _target.CreateReconciliationReportAsync(Fixture.Create<CreateReconciliationReportCommand>());

            // Assert
            actual.Should().BeOfType<AcceptedResult>();
            var objectResponse = actual as AcceptedResult;
            objectResponse?.StatusCode.Should().Be(202);
        }

        [Fact]
        public async Task Given_BeamApiException_On_CreateReconciliationReportAsync_Should_Throw_To_Middleware()
        {
            // Arrange
            MockMediator
                .Setup(x => x.Send(It.IsAny<CreateReconciliationReportCommand>(), default))
                .ThrowsAsync(new BeamApiException());

            // Act & Assert
            await Assert.ThrowsAsync<BeamApiException>(async () =>
                await _target.CreateReconciliationReportAsync(Fixture.Create<CreateReconciliationReportCommand>())
            );
        }

        [Fact]
        public async Task Given_Exception_On_CreateReconciliationReportAsync_Should_Return_500()
        {
            // Arrange
            MockMediator
                .Setup(x => x.Send(It.IsAny<CreateReconciliationReportCommand>(), default))
                .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.CreateReconciliationReportAsync(Fixture.Create<CreateReconciliationReportCommand>());

            // Assert
            actual.Should().BeOfType<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_On_GetRewardActivityAsync_Should_Return_200()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var response = Fixture.Create<Pagination<RewardActivity>>();
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetRewardActivityQuery>(), default))
                .ReturnsAsync(response);

            // Act
            var actual = await _target.GetRewardActivityAsync(customerId, 1, 10, Regions.Australia);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Null_Result_On_GetRewardActivityAsync_Should_Return_204()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetRewardActivityQuery>(), default))
                .ReturnsAsync(null as Pagination<RewardActivity>);

            // Act
            var actual = await _target.GetRewardActivityAsync(customerId, 1, 10, Regions.Australia);

            // Assert
            actual.Should().BeOfType<NoContentResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Given_Exception_On_GetRewardActivityAsync_Should_Return_500()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetRewardActivityQuery>(), default))
                .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetRewardActivityAsync(customerId, 1, 10, Regions.Australia);

            // Assert
            actual.Should().BeOfType<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_RefitException_On_GetRewardActivityAsync_Should_Return_424()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            MockMediator
                .Setup(x => x.Send(It.IsAny<GetRewardActivityQuery>(), default))
                .ThrowsAsync(new BeamApiException());

            // Act & Assert
            await Assert.ThrowsAsync<BeamApiException>(async () =>
                await _target.GetRewardActivityAsync(customerId, 1, 10, Regions.Australia)
            );
        }

        [Fact]
        public async Task Given_Valid_Request_On_PollReconciliationReportAsync_Should_Return_200()
        {
            // Arrange
            var uuid = Guid.NewGuid();
            var response = Fixture.Create<PollReconciliationReportResponse>();
            MockMediator
                .Setup(x => x.Send(It.IsAny<PollReconciliationReportQuery>(), default))
                .ReturnsAsync(response);

            // Act
            var actual = await _target.PollReconciliationReportAsync(uuid);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_RefitException_On_PollReconciliationReportAsync_Should_Return_424()
        {
            // Arrange
            var uuid = Guid.NewGuid();
            MockMediator
                .Setup(x => x.Send(It.IsAny<PollReconciliationReportQuery>(), default))
                .ThrowsAsync(new BeamApiException());

            // Act & Assert
            await Assert.ThrowsAsync<BeamApiException>(async () =>
                await _target.PollReconciliationReportAsync(uuid)
            );
        }

        [Fact]
        public async Task Given_Exception_On_PollReconciliationReportAsync_Should_Return_500()
        {
            // Arrange
            var uuid = Guid.NewGuid();
            MockMediator
                .Setup(x => x.Send(It.IsAny<PollReconciliationReportQuery>(), default))
                .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.PollReconciliationReportAsync(uuid);

            // Assert
            actual.Should().BeOfType<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_On_GetTransactionRewardDetailsAsync_Should_Return_200()
        {
            // Arrange
            var request = Fixture.Create<GetTransactionRewardDetailsQuery>();
            var response = Fixture.Build<TransactionRewardDetailsResponse>()
                                  .With(x => x.ZipTransactionId, request.TransactionId)
                                  .Create();
            MockMediator
                .Setup(x => x.Send(It.Is<GetTransactionRewardDetailsQuery>(x => x == request), default))
                .ReturnsAsync(response);

            // Act
            var actual = await _target.GetTransactionRewardDetailsAsync(request);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Null_Result_On_GetTransactionRewardDetailsAsync_Should_Return_204()
        {
            // Arrange
            var request = Fixture.Create<GetTransactionRewardDetailsQuery>();
            MockMediator
                .Setup(x => x.Send(It.Is<GetTransactionRewardDetailsQuery>(x => x == request), default))
                .ReturnsAsync(null as TransactionRewardDetailsResponse);

            // Act
            var actual = await _target.GetTransactionRewardDetailsAsync(request);

            // Assert
            actual.Should().BeOfType<NoContentResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Given_Exception_On_GetTransactionRewardDetailsAsync_Should_Return_500()
        {
            // Arrange
            var request = Fixture.Create<GetTransactionRewardDetailsQuery>();
            MockMediator
                .Setup(x => x.Send(It.Is<GetTransactionRewardDetailsQuery>(x => x == request), default))
                .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetTransactionRewardDetailsAsync(request);

            // Assert
            actual.Should().BeOfType<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }
    }
}
