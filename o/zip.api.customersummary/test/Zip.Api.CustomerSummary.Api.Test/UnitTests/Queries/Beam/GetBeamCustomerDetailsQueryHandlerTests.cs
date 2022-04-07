using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetCustomerDetails;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Beam
{
    public class GetBeamCustomerDetailsQueryHandlerTests : CommonTestsFixture
    {
        private readonly GetCustomerDetailsQueryHandler _target;

        private readonly Mock<IBeamService> _beamService;

        public GetBeamCustomerDetailsQueryHandlerTests()
        {
            _beamService = new Mock<IBeamService>();
            var logger = new Mock<ILogger<GetCustomerDetailsQueryHandler>>();

            _target = new GetCustomerDetailsQueryHandler(_beamService.Object, logger.Object);
        }

        [Fact]
        public async Task Given_Valid_Request_Should_Call_BeamService_GetCustomerDetails()
        {
            // Arrange
            var request = Fixture.Build<GetCustomerDetailsQuery>()
                                 .With(x => x.CustomerId, Guid.NewGuid())
                                 .With(x => x.Region, Regions.Australia)
                                 .Create();

            // Act
            await _target.Handle(request, default);

            // Assert
            _beamService.Verify(x =>
                                    x.GetCustomerDetails(
                                        It.Is<Guid>(y => y == request.CustomerId),
                                        It.Is<string>(y => y == request.Region)
                                    ),
                                Times.Once);
        }

        [Fact]
        public async Task Given_Exception_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<GetCustomerDetailsQuery>();

            _beamService.Setup(x => x.GetCustomerDetails(request.CustomerId, request.Region))
                        .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => { await _target.Handle(request, default); });
        }
    }
}