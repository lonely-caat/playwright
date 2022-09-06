using System;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Mfa.Query;
using Zip.Api.CustomerSummary.Infrastructure.Services.MfaService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Mfa
{
    public class GetMfaSmsDataQueryHandlerTests : CommonTestsFixture
    {
        private readonly GetMfaSmsDataQueryHandler _target;

        private readonly Mock<IMfaService> _mfaService;

        public GetMfaSmsDataQueryHandlerTests()
        {
            _mfaService = new Mock<IMfaService>();

            _target = new GetMfaSmsDataQueryHandler(_mfaService.Object);
        }

        [Fact]
        public async Task Given_Valid_Request_Should_Call_MfaService_GetMfaSmsDataQuery()
        {
            // Arrange
            var request = Fixture.Build<GetMfaSmsDataQuery>()
                .With(x => x.ConsumerId, 1234)
                .Create();

            // Act
            await _target.Handle(request, default);

            // Assert
            _mfaService.Verify(x =>
                x.GetMfaSmsDataAsync(
                    It.Is<long>(y => y == request.ConsumerId)
                ), Times.Once);
        }
    }
}