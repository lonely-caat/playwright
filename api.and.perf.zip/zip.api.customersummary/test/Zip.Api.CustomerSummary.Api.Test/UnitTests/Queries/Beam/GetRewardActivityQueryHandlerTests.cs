using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetRewardActivity;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Beam
{
    public class GetRewardActivityQueryHandlerTests : CommonTestsFixture
    {
        private readonly GetRewardActivityQueryHandler _target;

        private readonly Mock<IBeamService> _beamService;

        public GetRewardActivityQueryHandlerTests()
        {
            _beamService = new Mock<IBeamService>();
            var logger = new Mock<ILogger<GetRewardActivityQueryHandler>>();

            _target = new GetRewardActivityQueryHandler(_beamService.Object, logger.Object, Mapper);
        }

        [Fact]
        public async Task Given_Valid_Request_Should_Call_BeamService_GetRewardActivityAsync()
        {
            // Arrange
            var request = Fixture.Build<GetRewardActivityQuery>()
                                 .With(x => x.CustomerId, Guid.NewGuid())
                                 .With(x => x.PageNumber, 1)
                                 .With(x => x.PageSize, 10)
                                 .With(x => x.Region, Regions.Australia)
                                 .Create();

            var mockElementsList = new List<RewardActivity>();
            mockElementsList.Add(new RewardActivity());
            _beamService
               .Setup(x => x.GetRewardActivityAsync(request.CustomerId, 0, request.PageSize, request.Region))
               .ReturnsAsync(new RewardActivityResponse()
                {
                    Count = 10,
                    TotalCount = 20,
                    HasMore = true,
                    Elements = mockElementsList
                });

            // Act
            await _target.Handle(request, default);

            // Assert
            _beamService.Verify(x =>
                                    x.GetRewardActivityAsync(
                                        It.Is<Guid>(y => y == request.CustomerId),
                                        It.Is<long>(y => y == 0),
                                        It.Is<long>(y => y == request.PageSize),
                                        It.Is<string>(y => y == Regions.Australia)
                                    ),
                                Times.Once);
        }

        [Fact]
        public async Task
            Given_Valid_Request_Should_Call_BeamService_And_Catch_Error_If_Thrown_In_GetRewardActivityAsync()
        {
            // Arrange
            var request = Fixture.Build<GetRewardActivityQuery>()
                                 .With(x => x.CustomerId, Guid.NewGuid())
                                 .With(x => x.PageNumber, 1)
                                 .With(x => x.PageSize, 10)
                                 .With(x => x.Region, Regions.Australia)
                                 .Create();

            _beamService
               .Setup(x => x.GetRewardActivityAsync(request.CustomerId, 0, request.PageSize, request.Region))
               .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _target.Handle(request, default));
            _beamService.Verify(x =>
                                    x.GetRewardActivityAsync(
                                        It.Is<Guid>(y => y == request.CustomerId),
                                        It.Is<long>(y => y == 0),
                                        It.Is<long>(y => y == request.PageSize),
                                        It.Is<string>(y => y == Regions.Australia)
                                    ),
                                Times.Once);
        }

        [Fact]
        public async Task Given_Null_Response_From_BeamService_GetRewardActivityAsync_Should_Return_Null()
        {
            // Arrange
            var request = Fixture.Build<GetRewardActivityQuery>()
                                 .With(x => x.CustomerId, Guid.NewGuid())
                                 .With(x => x.PageNumber, 1)
                                 .With(x => x.PageSize, 10)
                                 .With(x => x.Region, Regions.Australia)
                                 .Create();

            _beamService
               .Setup(x => x.GetRewardActivityAsync(request.CustomerId, 0, request.PageSize, request.Region))
               .ReturnsAsync(null as RewardActivityResponse);

            // Act
            var result = await _target.Handle(request, default);

            // Assert
            Assert.Null(result);
            _beamService.Verify(x =>
                                    x.GetRewardActivityAsync(
                                        It.Is<Guid>(y => y == request.CustomerId),
                                        It.Is<long>(y => y == 0),
                                        It.Is<long>(y => y == request.PageSize),
                                        It.Is<string>(y => y == Regions.Australia)
                                    ),
                                Times.Once);
        }

        [Fact]
        public async Task Given_Exception_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<GetRewardActivityQuery>();

            _beamService
               .Setup(x => x.GetRewardActivityAsync(request.CustomerId,
                                                    It.IsAny<long>(),
                                                    It.IsAny<long>(),
                                                    request.Region))
               .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => { await _target.Handle(request, default); });
        }
    }
}