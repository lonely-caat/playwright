using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCards;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.VcnCards
{
    public class GetCardsQueryHandlerTests : CommonTestsFixture
    {
        private readonly Mock<IVcnCardService> _vcnCardService;

        private readonly GetCardsQueryHandler _target;

        public GetCardsQueryHandlerTests()
        {
            _vcnCardService = new Mock<IVcnCardService>();
            _target = new GetCardsQueryHandler(_vcnCardService.Object);
        }

        [Fact]
        public async Task Handle_Should_Invoke_VcnCardService_Correctly()
        {
            // Arrange
            var request = Fixture.Create<GetCardsQuery>();
            var response = new RootCards { Cards = Fixture.CreateMany<Card>().ToList() };

            _vcnCardService.Setup(x => x.GetCardsAsync(request.CustomerId, request.AccountId, default)).ReturnsAsync(response);

            // Act
            await _target.Handle(request, CancellationToken);

            // Assert
            _vcnCardService.Verify(x => x.GetCardsAsync(request.CustomerId, request.AccountId, CancellationToken), Times.Once);
        }
        
        [Fact]
        public async Task Handle_Should_Filter_Out_EML_VcnCards()
        {
            // Arrange
            var request = Fixture.Create<GetCardsQuery>();
            var emlCards = Fixture.Build<Card>().With(x => x.Type, nameof(VcnCardType.ONLINE)).CreateMany().ToList();
            var cards = Fixture.CreateMany<Card>().ToList();
            cards.AddRange(emlCards);
            var response = new RootCards {Cards = cards};
            var expectedCardsCount = cards.Count - emlCards.Count;

            _vcnCardService.Setup(x => x.GetCardsAsync(request.CustomerId, request.AccountId, default)).ReturnsAsync(response);

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.Cards.Should().NotContain(x => x.Type == nameof(VcnCardType.ONLINE));
            actual.Cards.Should().HaveCount(expectedCardsCount);
        }

        [Fact]
        public async Task Handle_Should_Order_VcnCards_By_CreatedOn_Desc()
        {
            // Arrange
            var request = Fixture.Create<GetCardsQuery>();
            var cards = Fixture.CreateMany<Card>(10).ToList();
            var response = new RootCards { Cards = cards };
            
            _vcnCardService.Setup(x => x.GetCardsAsync(request.CustomerId, request.AccountId, default)).ReturnsAsync(response);

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.Cards.Should().NotContain(x => x.Type == nameof(VcnCardType.ONLINE));
            actual.Cards.Should().BeInDescendingOrder(x => x.CreatedOn);
        }
    }
}
