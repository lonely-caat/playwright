using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Beam.Command.CreateReconciliationReport;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetCustomerDetails;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetTransactionRewardDetails;
using Zip.Api.CustomerSummary.Application.Beam.Query.PollReconciliationReport;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class BeamServiceTests : CommonTestsFixture
    {
        private readonly BeamService _target;

        private readonly Mock<IBeamProxy> _beamProxy;

        public BeamServiceTests()
        {
            _beamProxy = new Mock<IBeamProxy>();
            var logger = new Mock<ILogger<BeamService>>();
            _target = new BeamService(_beamProxy.Object, logger.Object);
        }

        [Fact]
        public async Task
            Given_Success_Response_From_BeamProxy_GetCustomerDetailsAsync_Should_Return_BeamCustomerDetails()
        {
            // Arrange
            var request = Fixture.Build<GetCustomerDetailsQuery>()
                                 .With(x => x.CustomerId, Guid.NewGuid())
                                 .Create();

            var response = Fixture.Build<CustomerDetails>()
                                  .With(x => x.CustomerId, request.CustomerId)
                                  .Create();

            _beamProxy.Setup(x => x.GetCustomerDetailsAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                      .ReturnsAsync(response);

            // Act
            var result = await _target.GetCustomerDetails(request.CustomerId);

            // Assert
            Assert.Equal(result.CustomerId, request.CustomerId);

            _beamProxy.Verify(x =>
                                  x.GetCustomerDetailsAsync(
                                      It.Is<Guid>(y => y == request.CustomerId),
                                      It.Is<string>(y => y == Regions.Australia)),
                              Times.Once);
        }

        [Fact]
        public async Task Given_NotFound_RefitApiException_GetCustomerDetailsAsync_Should_Return_Null()
        {
            // Arrange
            var request = Fixture.Build<GetCustomerDetailsQuery>()
                                 .With(x => x.CustomerId, Guid.NewGuid())
                                 .Create();

            var refitNotFoundException = await Refit.ApiException.Create(
                                             new HttpRequestMessage(),
                                             HttpMethod.Get,
                                             new HttpResponseMessage(HttpStatusCode.NotFound)
                                         );

            _beamProxy.Setup(x => x.GetCustomerDetailsAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                      .ThrowsAsync(refitNotFoundException);

            // Act
            var result = await _target.GetCustomerDetails(request.CustomerId);

            // Assert
            Assert.Null(result);

            _beamProxy.Verify(x =>
                                  x.GetCustomerDetailsAsync(
                                      It.Is<Guid>(y => y == request.CustomerId),
                                      It.Is<string>(y => y == Regions.Australia)),
                              Times.Once);
        }

        [Theory()]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task Given_Non404_RefitApiException_GetCustomerDetailsAsync_Should_Throw_BeamApiException(
            HttpStatusCode statusCode)
        {
            // Arrange
            var request = Fixture.Build<GetCustomerDetailsQuery>()
                                 .With(x => x.CustomerId, Guid.NewGuid())
                                 .Create();

            var refitException = await Refit.ApiException.Create(
                                     new HttpRequestMessage(),
                                     HttpMethod.Get,
                                     new HttpResponseMessage(statusCode)
                                 );

            _beamProxy.Setup(x => x.GetCustomerDetailsAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                      .ThrowsAsync(refitException);

            // Act & Assert
            await Assert.ThrowsAsync<BeamApiException>(async () =>
                                                           await _target.GetCustomerDetails(request.CustomerId)
            );
        }

        [Fact]
        public async Task Given_Exception_Caught_GetCustomerDetailsAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Build<GetCustomerDetailsQuery>()
                                 .With(x => x.CustomerId, Guid.NewGuid())
                                 .Create();

            _beamProxy.Setup(x => x.GetCustomerDetailsAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                      .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                                                    await _target.GetCustomerDetails(request.CustomerId)
            );
        }

        [Fact]
        public async Task Given_Success_Response_From_BeamProxy_CreateReconciliationReportAsync_Should_Return()
        {
            // Arrange
            var request = Fixture.Build<CreateReconciliationReportCommand>()
                                 .With(x => x.SelectedDate, new DateTime())
                                 .With(x => x.RequestedBy, "test.user@zip.co")
                                 .With(x => x.Region, Regions.Australia)
                                 .Create();

            var response = Fixture.Build<CreateReconciliationReportResponse>()
                                  .With(x => x.Uuid, Guid.NewGuid)
                                  .Create();

            _beamProxy.Setup(x => x.CreateReconciliationReportAsync(It.IsAny<long>(),
                                                                    It.IsAny<long>(),
                                                                    It.IsAny<string>(),
                                                                    It.IsAny<string>()))
                      .ReturnsAsync(response);

            // Act
            var result =
                await _target.CreateReconciliationReportAsync(request.SelectedDate,
                                                              request.RequestedBy,
                                                              request.Region);

            // Assert
            Assert.Equal(result.Uuid, response.Uuid);

            _beamProxy.Verify(x =>
                                  x.CreateReconciliationReportAsync(
                                      It.Is<long>(y => y == request.SelectedDate.Year),
                                      It.Is<long>(y => y == request.SelectedDate.Month),
                                      It.Is<string>(y => y == request.RequestedBy),
                                      It.Is<string>(y => y == request.Region)),
                              Times.Once);
        }

        [Theory()]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task Given_RefitApiException_CreateReconciliationReportAsync_Should_Throw_BeamApiException(
            HttpStatusCode statusCode)
        {
            // Arrange
            var request = Fixture.Build<CreateReconciliationReportCommand>()
                                 .With(x => x.SelectedDate, new DateTime())
                                 .With(x => x.RequestedBy, "test.user@zip.co")
                                 .With(x => x.Region, Regions.Australia)
                                 .Create();

            var refitException = await Refit.ApiException.Create(
                                     new HttpRequestMessage(),
                                     HttpMethod.Get,
                                     new HttpResponseMessage(statusCode)
                                 );

            _beamProxy.Setup(x => x.CreateReconciliationReportAsync(It.IsAny<long>(),
                                                                    It.IsAny<long>(),
                                                                    It.IsAny<string>(),
                                                                    It.IsAny<string>()))
                      .ThrowsAsync(refitException);

            // Act & Assert
            await Assert.ThrowsAsync<BeamApiException>(async () =>
                                                           await _target.CreateReconciliationReportAsync(
                                                               request.SelectedDate,
                                                               request.RequestedBy,
                                                               request.Region)
            );
        }

        [Fact]
        public async Task Given_Exception_Caught_CreateReconciliationReportAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Build<CreateReconciliationReportCommand>()
                                 .With(x => x.SelectedDate, new DateTime())
                                 .With(x => x.RequestedBy, "test.user@zip.co")
                                 .With(x => x.Region, Regions.Australia)
                                 .Create();

            _beamProxy.Setup(x => x.CreateReconciliationReportAsync(It.IsAny<long>(),
                                                                    It.IsAny<long>(),
                                                                    It.IsAny<string>(),
                                                                    It.IsAny<string>()))
                      .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                                                    await _target.CreateReconciliationReportAsync(
                                                        request.SelectedDate,
                                                        request.RequestedBy,
                                                        request.Region)
            );
        }

        [Fact]
        public async Task Given_Success_Response_From_BeamProxy_PollReconciliationReportAsync_Should_Return()
        {
            // Arrange
            var request = Fixture.Build<PollReconciliationReportQuery>()
                                 .With(x => x.Uuid, Guid.NewGuid)
                                 .With(x => x.RequestedBy, "test.user@zip.co")
                                 .Create();

            var response = Fixture.Build<PollReconciliationReportResponse>()
                                  .With(x => x.Uuid, request.Uuid)
                                  .With(x => x.Complete, true)
                                  .With(x => x.Url, "testurl")
                                  .Create();

            _beamProxy.Setup(x => x.PollReconciliationReportAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                      .ReturnsAsync(response);

            // Act
            var result = await _target.PollReconciliationReportAsync(request.Uuid, request.RequestedBy);

            // Assert
            Assert.Equal(result.Uuid, request.Uuid);

            _beamProxy.Verify(x =>
                                  x.PollReconciliationReportAsync(
                                      It.Is<Guid>(y => y == request.Uuid),
                                      It.Is<string>(y => y == request.RequestedBy)),
                              Times.Once);
        }

        [Theory()]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task Given_RefitApiException_PollReconciliationReportAsync_Should_Throw_BeamApiException(
            HttpStatusCode statusCode)
        {
            // Arrange
            var request = Fixture.Build<PollReconciliationReportQuery>()
                                 .With(x => x.Uuid, Guid.NewGuid)
                                 .With(x => x.RequestedBy, "test.user@zip.co")
                                 .Create();

            var refitException = await Refit.ApiException.Create(
                                     new HttpRequestMessage(),
                                     HttpMethod.Get,
                                     new HttpResponseMessage(statusCode)
                                 );

            _beamProxy.Setup(x => x.PollReconciliationReportAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                      .ThrowsAsync(refitException);

            // Act & Assert
            await Assert.ThrowsAsync<BeamApiException>(async () =>
                                                           await _target.PollReconciliationReportAsync(
                                                               request.Uuid,
                                                               request.RequestedBy)
            );
        }

        [Fact]
        public async Task Given_Exception_Caught_PollReconciliationReportAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Build<PollReconciliationReportQuery>()
                                 .With(x => x.Uuid, Guid.NewGuid)
                                 .With(x => x.RequestedBy, "test.user@zip.co")
                                 .Create();

            _beamProxy.Setup(x => x.PollReconciliationReportAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                      .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                                                    await _target.PollReconciliationReportAsync(
                                                        request.Uuid,
                                                        request.RequestedBy)
            );
        }

        [Fact]
        public async Task Given_Success_Response_From_BeamProxy_GetTransactionRewardDetailsAsync_Should_Return()
        {
            // Arrange
            var request = Fixture.Create<GetTransactionRewardDetailsQuery>();

            var response = Fixture.Build<TransactionRewardDetailsResponse>()
                                  .With(x => x.ZipTransactionId, request.TransactionId)
                                  .Create();

            _beamProxy.Setup(x => x.GetTransactionRewardDetailsAsync(
                                 It.Is<Guid>(y => y == request.CustomerId),
                                 It.Is<long>(y => y == request.TransactionId)))
                      .ReturnsAsync(response);

            // Act
            var result = await _target.GetTransactionRewardDetailsAsync(request.CustomerId, request.TransactionId);

            // Assert
            Assert.Equal(request.TransactionId, result.ZipTransactionId);

            _beamProxy.Verify(x =>
                                  x.GetTransactionRewardDetailsAsync(
                                      It.Is<Guid>(y => y == request.CustomerId),
                                      It.Is<long>(y => y == request.TransactionId)),
                              Times.Once);
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task Given_RefitApiException_GetTransactionRewardDetailsAsync_Should_Throw_BeamApiException(
            HttpStatusCode statusCode)
        {
            // Arrange
            var request = Fixture.Create<GetTransactionRewardDetailsQuery>();

            var refitException = await Refit.ApiException.Create(
                                     new HttpRequestMessage(),
                                     HttpMethod.Get,
                                     new HttpResponseMessage(statusCode)
                                 );

            _beamProxy.Setup(x => x.GetTransactionRewardDetailsAsync(
                                 It.Is<Guid>(y => y == request.CustomerId),
                                 It.Is<long>(y => y == request.TransactionId)))
                      .ThrowsAsync(refitException);

            // Act & Assert
            await Assert.ThrowsAsync<BeamApiException>(async () =>
                                                           await _target.GetTransactionRewardDetailsAsync(
                                                               request.CustomerId,
                                                               request.TransactionId)
            );
        }

        [Fact]
        public async Task Given_RefitApiException_NotFound_GetTransactionRewardDetailsAsync_Should_Return_Null()
        {
            // Arrange
            var request = Fixture.Create<GetTransactionRewardDetailsQuery>();

            var refitNotFoundException = await Refit.ApiException.Create(
                                             new HttpRequestMessage(),
                                             HttpMethod.Get,
                                             new HttpResponseMessage(HttpStatusCode.NotFound)
                                         );

            _beamProxy.Setup(x => x.GetTransactionRewardDetailsAsync(
                                 It.Is<Guid>(y => y == request.CustomerId),
                                 It.Is<long>(y => y == request.TransactionId)))
                      .ThrowsAsync(refitNotFoundException);

            // Act
            var result = await _target.GetTransactionRewardDetailsAsync(request.CustomerId, request.TransactionId);

            // Assert
            Assert.Null(result);

            _beamProxy.Verify(x =>
                                  x.GetTransactionRewardDetailsAsync(
                                      It.Is<Guid>(y => y == request.CustomerId),
                                      It.Is<long>(y => y == request.TransactionId)),
                              Times.Once);
        }

        [Fact]
        public async Task Given_Exception_Caught_GetTransactionRewardDetailsAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<GetTransactionRewardDetailsQuery>();

            _beamProxy.Setup(x => x.GetTransactionRewardDetailsAsync(
                                 It.Is<Guid>(y => y == request.CustomerId),
                                 It.Is<long>(y => y == request.TransactionId)))
                      .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                                                    await _target.GetTransactionRewardDetailsAsync(
                                                        request.CustomerId,
                                                        request.TransactionId)
            );
        }

        [Fact]
        public async Task Given_Valid_Parameters_GetRewardActivityAsync_Should_Return()
        {
            // Arrange
            var customerId = Fixture.Create<Guid>();
            const int defaultSkipAndTake = 1;
            var response = Fixture.Create<RewardActivityResponse>();

            _beamProxy.Setup(x => x.GetRewardActivityAsync(customerId,
                                                           defaultSkipAndTake,
                                                           defaultSkipAndTake,
                                                           Regions.Australia))
                      .ReturnsAsync(response);

            // Act
            var actual =
                await _target.GetRewardActivityAsync(customerId,
                                                     defaultSkipAndTake,
                                                     defaultSkipAndTake);

            // Assert
            Assert.Equal(response.ToString(), actual.ToString());
        }

        [Fact]
        public async Task Given_RefitException_NotFound_GetRewardActivityAsync_Should_Return_Null()
        {
            // Arrange
            var customerId = Fixture.Create<Guid>();
            const int defaultSkipAndTake = 1;

            var refitNotFoundException = await Refit.ApiException.Create(
                                             new HttpRequestMessage(),
                                             HttpMethod.Get,
                                             new HttpResponseMessage(HttpStatusCode.NotFound)
                                         );

            _beamProxy.Setup(x => x.GetRewardActivityAsync(customerId,
                                                           defaultSkipAndTake,
                                                           defaultSkipAndTake,
                                                           Regions.Australia))
                      .ThrowsAsync(refitNotFoundException);

            // Act
            var actual = await _target.GetRewardActivityAsync(customerId, defaultSkipAndTake, defaultSkipAndTake);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Given_RefitException_GetRewardActivityAsync_Should_Throw_BeamApiException()
        {
            // Arrange
            var customerId = Fixture.Create<Guid>();
            const int defaultSkipAndTake = 1;

            var refitException = await Refit.ApiException.Create(
                                     new HttpRequestMessage(),
                                     HttpMethod.Get,
                                     new HttpResponseMessage(HttpStatusCode.InternalServerError)
                                 );

            _beamProxy.Setup(x => x.GetRewardActivityAsync(customerId,
                                                           defaultSkipAndTake,
                                                           defaultSkipAndTake,
                                                           Regions.Australia))
                      .ThrowsAsync(refitException);

            // Act & Assert
            await Assert.ThrowsAsync<BeamApiException>(async () =>
                                                           await _target.GetRewardActivityAsync(
                                                               customerId,
                                                               defaultSkipAndTake,
                                                               defaultSkipAndTake)
            );
        }

        [Fact]
        public async Task Given_Exception_GetRewardActivityAsync_Should_Throw()
        {
            // Arrange
            var customerId = Fixture.Create<Guid>();
            const int defaultSkipAndTake = 1;

            _beamProxy.Setup(x => x.GetRewardActivityAsync(customerId,
                                                           defaultSkipAndTake,
                                                           defaultSkipAndTake,
                                                           Regions.Australia))
                      .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                                                    await _target.GetRewardActivityAsync(
                                                        customerId,
                                                        defaultSkipAndTake,
                                                        defaultSkipAndTake)
            );
        }
    }
}