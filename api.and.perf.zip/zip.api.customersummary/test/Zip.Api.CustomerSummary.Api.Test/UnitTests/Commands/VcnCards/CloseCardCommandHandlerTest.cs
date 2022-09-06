using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.CloseCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.VcnCards
{
    public class CloseCardCommandHandlerTest : CommonTestsFixture
    {
        private readonly Mock<IVcnCardService> _vcnCardService;

        private readonly CloseCardCommandHandler _target;

        public CloseCardCommandHandlerTest()
        {
            _vcnCardService = new Mock<IVcnCardService>();
            _target = new CloseCardCommandHandler(_vcnCardService.Object);
        }

        [Fact]
        public async Task Handle_Should_Invoke_VcnCardService_Correctly()
        {
            // Arrange
            var request = Fixture.Create<CloseCardCommand>();

            // Act
            await _target.Handle(request, CancellationToken);

            // Assert
            _vcnCardService.Verify(x => x.CloseCardAsync(request.CardId, CancellationToken), Times.Once);
        }
    }
}
