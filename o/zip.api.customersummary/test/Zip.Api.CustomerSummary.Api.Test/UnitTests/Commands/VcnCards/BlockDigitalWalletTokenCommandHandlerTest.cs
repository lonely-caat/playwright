﻿using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockDigitalWalletToken;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.VcnCards
{
    public class BlockDigitalWalletTokenCommandHandlerTest : CommonTestsFixture
    {
        private readonly Mock<IVcnCardService> _vcnCardService;

        private readonly BlockDigitalWalletTokenCommandHandler _target;

        public BlockDigitalWalletTokenCommandHandlerTest()
        {
            _vcnCardService = new Mock<IVcnCardService>();
            _target = new BlockDigitalWalletTokenCommandHandler(_vcnCardService.Object, Mapper);
        }

        [Fact]
        public async Task Handle_Should_Invoke_VcnCardService_Correctly()
        {
            // Arrange
            var request = Fixture.Create<BlockDigitalWalletTokenCommand>();
            var expectedTokenTransitionRequest = Fixture.Build<TokenTransitionRequest>()
                                                        .With(x => x.DigitalWalletToken, request.DigitalWalletToken)
                                                        .With(x => x.Provider, Vcn.DigitalWalletTokenProvider)
                                                        .With(x => x.State, nameof(DigitalWalletTokenState.SUSPENDED))
                                                        .With(x => x.ReasonCode, Vcn.DigitalWalletTokenTransitionReasonCode_01)
                                                        .Create();

            // Act
            await _target.Handle(request, CancellationToken);

            // Assert
            _vcnCardService.Verify(x => x.SendTokenTransitionRequestAsync(
                                       It.Is<TokenTransitionRequest>(
                                           y => y.DigitalWalletToken == expectedTokenTransitionRequest.DigitalWalletToken &&
                                                y.Provider == expectedTokenTransitionRequest.Provider &
                                                y.ReasonCode == expectedTokenTransitionRequest.ReasonCode &&
                                                y.State == expectedTokenTransitionRequest.State),
                                       CancellationToken),
                                   Times.Once);
        }
    }
}
