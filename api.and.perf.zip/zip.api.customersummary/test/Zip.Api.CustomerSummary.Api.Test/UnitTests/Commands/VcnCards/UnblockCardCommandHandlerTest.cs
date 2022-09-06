using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.VcnCards
{
    public class UnblockCardCommandHandlerTest : CommonTestsFixture
    {
        private readonly Mock<IVcnCardService> _vcnCardService;

        private readonly UnblockCardCommandHandler _target;

        public UnblockCardCommandHandlerTest()
        {
            _vcnCardService = new Mock<IVcnCardService>();
            _target = new UnblockCardCommandHandler(_vcnCardService.Object);
        }

        [Fact]
        public async Task Handle_Should_Invoke_VcnCardService_Correctly()
        {
            // Arrange
            var request = Fixture.Create<UnblockCardCommand>();

            // Act
            await _target.Handle(request, CancellationToken);

            // Assert
            _vcnCardService.Verify(x => x.UnblockCardAsync(request.CardId, CancellationToken), Times.Once);
        }
    }
}
