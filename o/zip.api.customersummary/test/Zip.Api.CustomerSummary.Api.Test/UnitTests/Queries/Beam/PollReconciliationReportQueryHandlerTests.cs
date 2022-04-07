using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Beam.Query.PollReconciliationReport;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Beam
{
    public class PollReconciliationReportQueryHandlerTests : CommonTestsFixture
    {
        private readonly PollReconciliationReportQueryHandler _target;

        private readonly Mock<IBeamService> _beamService;

        public PollReconciliationReportQueryHandlerTests()
        {
            _beamService = new Mock<IBeamService>();
            var logger = new Mock<ILogger<PollReconciliationReportQueryHandler>>();

            _target = new PollReconciliationReportQueryHandler(_beamService.Object, logger.Object);
        }

        [Fact]
        public async Task Given_Valid_Request_Should_Call_BeamService_PollReconciliationReportAsync()
        {
            // Arrange
            var request = Fixture.Build<PollReconciliationReportQuery>()
                                 .With(x => x.Uuid, Guid.NewGuid())
                                 .With(x => x.RequestedBy, "test.user@zip.co")
                                 .Create();

            // Act
            await _target.Handle(request, default);

            // Assert
            _beamService.Verify(x =>
                                    x.PollReconciliationReportAsync(
                                        It.Is<Guid>(y => y == request.Uuid),
                                        It.Is<string>(y => y == request.RequestedBy)
                                    ),
                                Times.Once);
        }

        [Fact]
        public async Task Given_Exception_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<PollReconciliationReportQuery>();

            _beamService.Setup(x => x.PollReconciliationReportAsync(request.Uuid, request.RequestedBy))
                        .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => { await _target.Handle(request, default); });
        }
    }
}