using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Beam.Command.CreateReconciliationReport;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Beam
{
    public class CreateReconciliationReportCommandHandlerTests : CommonTestsFixture
    {
        private readonly CreateReconciliationReportCommandHandler _target;

        private readonly Mock<IBeamService> _beamService;

        public CreateReconciliationReportCommandHandlerTests()
        {
            _beamService = new Mock<IBeamService>();
            var logger = new Mock<ILogger<CreateReconciliationReportCommandHandler>>();

            _target = new CreateReconciliationReportCommandHandler(_beamService.Object, logger.Object);
        }

        [Fact]
        public async Task Given_Valid_Request_Should_Call_BeamService_CreateReconciliationReportAsync()
        {
            // Arrange
            var request = Fixture.Build<CreateReconciliationReportCommand>()
                                 .With(x => x.SelectedDate, new DateTime())
                                 .With(x => x.RequestedBy, "test.user@zip.co")
                                 .With(x => x.Region, Regions.Australia)
                                 .Create();

            // Act
            await _target.Handle(request, default);

            // Assert
            _beamService.Verify(x =>
                                    x.CreateReconciliationReportAsync(
                                        It.Is<DateTime>(y => y == request.SelectedDate),
                                        It.Is<string>(y => y == request.RequestedBy),
                                        It.Is<string>(y => y == request.Region)),
                                Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_BeamService_GetTransactionRewardDetailsAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<CreateReconciliationReportCommand>();

            _beamService.Setup(x =>
                                   x.CreateReconciliationReportAsync(
                                       It.Is<DateTime>(y => y == request.SelectedDate),
                                       It.Is<string>(y => y == request.RequestedBy),
                                       It.Is<string>(y => y == request.Region)))
                        .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _target.Handle(request, default));
        }
    }
}