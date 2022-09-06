using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetTransactionRewardDetails;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Beam
{
    public class GetTransactionRewardDetailsQueryHandlerTests : CommonTestsFixture
    {
        private readonly GetTransactionRewardDetailsQueryHandler _target;

        private readonly Mock<IBeamService> _beamService;

        public GetTransactionRewardDetailsQueryHandlerTests()
        {
            var logger = new Mock<ILogger<GetTransactionRewardDetailsQueryHandler>>();
            _beamService = new Mock<IBeamService>();

            _target = new GetTransactionRewardDetailsQueryHandler(_beamService.Object, logger.Object);
        }

        [Fact]
        public async Task Given_Valid_Request_Should_Call_BeamService_GetTransactionRewardDetailsAsync()
        {
            // Arrange
            var request = Fixture.Create<GetTransactionRewardDetailsQuery>();

            // Act
            await _target.Handle(request, default);

            // Assert
            _beamService.Verify(x => x.GetTransactionRewardDetailsAsync(
                                    It.Is<Guid>(y => y == request.CustomerId),
                                    It.Is<long>(y => y == request.TransactionId)),
                                Times.Once);
        }

        [Fact]
        public async Task Given_Exception_On_BeamService_GetTransactionRewardDetailsAsync_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<GetTransactionRewardDetailsQuery>();

            _beamService.Setup(x =>
                                   x.GetTransactionRewardDetailsAsync(
                                       It.Is<Guid>(y => y == request.CustomerId),
                                       It.Is<long>(y => y == request.TransactionId)))
                        .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _target.Handle(request, default));
        }
    }
}