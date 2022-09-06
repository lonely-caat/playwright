using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.VcnCards
{
    public class GetCardQueryHandlerTests : CommonTestsFixture
    {
        private readonly Mock<IVcnCardService> _vcnCardService;

        private readonly GetCardQueryHandler _target;

        public GetCardQueryHandlerTests()
        {
            _vcnCardService = new Mock<IVcnCardService>();
            _target = new GetCardQueryHandler(_vcnCardService.Object);
        }

        [Fact]
        public async Task Handle_Should_Invoke_GetCardAsync_Given_CardId()
        {
            // Arrange
            var request = Fixture.Build<GetCardQuery>().Without(x => x.ExternalId).Create();

            // Act
            await _target.Handle(request, CancellationToken);

            // Assert
            _vcnCardService.Verify(x => x.GetCardAsync(request.CardId, CancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Invoke_GetCardByExternalIdAsync_Given_CardId()
        {
            // Arrange
            var request = Fixture.Build<GetCardQuery>().Without(x => x.CardId).Create();

            // Act
            await _target.Handle(request, CancellationToken);

            // Assert
            _vcnCardService.Verify(x => x.GetCardByExternalIdAsync(request.ExternalId, CancellationToken), Times.Once);
        }
    }
}
