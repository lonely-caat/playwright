using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.VcnCards
{
    public class BlockCardCommandHandlerTest : CommonTestsFixture
    {
        private readonly Mock<IVcnCardService> _vcnCardService;

        private readonly BlockCardCommandHandler _target; 

        public BlockCardCommandHandlerTest()
        {
            _vcnCardService = new Mock<IVcnCardService>();
            _target = new BlockCardCommandHandler(_vcnCardService.Object);
        }

        [Fact]
        public async Task Handle_Should_Invoke_VcnCardService_Correctly()
        {
            // Arrange
            var request = Fixture.Create<BlockCardCommand>();
            
            // Act
            await _target.Handle(request, CancellationToken);
            
            // Assert
            _vcnCardService.Verify(x => x.BlockCardAsync(request.CardId, CancellationToken), Times.Once);
        }
    }
}
