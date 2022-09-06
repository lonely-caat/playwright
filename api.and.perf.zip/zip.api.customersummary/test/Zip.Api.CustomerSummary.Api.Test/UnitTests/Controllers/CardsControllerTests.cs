using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockCard;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.CloseCard;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.TerminateDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockCard;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.VerifyDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCard;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCards;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCardTransactions;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class CardsControllerTests : CommonTestsFixture
    {
        private readonly CardsController _target;

        public CardsControllerTests()
        {
            _target = new CardsController(MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Input_When_GetAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<GetCardQuery>();
            var expect = Fixture.Create<Card>();

            MockMediator.Setup(x => x.Send(request, default))
                        .ReturnsAsync(expect);

            // Act
            var actual = await _target.GetAsync(request);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Valid_Input_When_GetAsync_Has_No_Result_Then_Should_Be_204()
        {
            // Arrange
            var request = Fixture.Create<GetCardQuery>();

            MockMediator.Setup(x => x.Send(request, default))
                        .ReturnsAsync(null as Card);

            // Act
            var actual = await _target.GetAsync(request);

            // Assert
            actual.Should().BeOfType<NoContentResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(204);
        }
        
        [Fact]
        public async Task Given_Handler_Throws_Exception_When_GetAllSnapshotMonthsAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<GetCardQuery>();
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_When_GetCardsAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<GetCardsQuery>();
            var expect = Fixture.Create<RootCards>();

            MockMediator.Setup(x => x.Send(request, default))
                        .ReturnsAsync(expect);

            // Act
            var actual = await _target.GetCardsAsync(request);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Valid_Input_When_GetCardsAsync_Has_No_Result_Then_Should_Be_204()
        {
            // Arrange
            var request = Fixture.Create<GetCardsQuery>();

            MockMediator.Setup(x => x.Send(request, default))
                        .ReturnsAsync(null as RootCards);

            // Act
            var actual = await _target.GetCardsAsync(request);

            // Assert
            actual.Should().BeOfType<NoContentResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(204);
        }
        
        [Fact]
        public async Task Given_Handler_Throws_Exception_When_GetCardsAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<GetCardsQuery>();
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetCardsAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_When_BlockCard_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<BlockCardCommand>();
            
            // Act
            var actual = await _target.BlockCardAsync(request);

            // Assert
            actual.Should().BeOfType<OkResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }
        
        [Fact]
        public async Task Given_Handler_Throws_Exception_When_BlockCard_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<BlockCardCommand>();
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.BlockCardAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_When_UnblockCard_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<UnblockCardCommand>();

            // Act
            var actual = await _target.UnblockCardAsync(request);

            // Assert
            actual.Should().BeOfType<OkResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_UnblockCard_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<UnblockCardCommand>();
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.UnblockCardAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_When_CloseCard_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<CloseCardCommand>();

            // Act
            var actual = await _target.CloseCardAsync(request);

            // Assert
            actual.Should().BeOfType<OkResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_CloseCard_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<CloseCardCommand>();
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.CloseCardAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_When_BlockDigitalWalletTokenAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<BlockDigitalWalletTokenCommand>();

            // Act
            var actual = await _target.BlockDigitalWalletTokenAsync(request);

            // Assert
            actual.Should().BeOfType<OkResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_BlockDigitalWalletTokenAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<BlockDigitalWalletTokenCommand>();
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.BlockDigitalWalletTokenAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }
        
        [Fact]
        public async Task Given_Valid_Input_When_UnblockDigitalWalletTokenAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<UnblockDigitalWalletTokenCommand>();

            // Act
            var actual = await _target.UnblockDigitalWalletTokenAsync(request);

            // Assert
            actual.Should().BeOfType<OkResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_UnblockDigitalWalletTokenAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<UnblockDigitalWalletTokenCommand>();
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.UnblockDigitalWalletTokenAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_When_TerminateDigitalWalletTokenAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<TerminateDigitalWalletTokenCommand>();

            // Act
            var actual = await _target.TerminateDigitalWalletTokenAsync(request);

            // Assert
            actual.Should().BeOfType<OkResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_TerminateDigitalWalletTokenAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<TerminateDigitalWalletTokenCommand>();
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.TerminateDigitalWalletTokenAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_When_VerifyDigitalWalletTokenAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<VerifyDigitalWalletTokenCommand>();

            // Act
            var actual = await _target.VerifyDigitalWalletTokenAsync(request);

            // Assert
            actual.Should().BeOfType<OkResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_VerifyDigitalWalletTokenAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<VerifyDigitalWalletTokenCommand>();
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.VerifyDigitalWalletTokenAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_When_GetCardTransactionsAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<GetCardTransactionsQuery>();
            MockMediator.Setup(x => x.Send(request, default))
                .ReturnsAsync(Fixture.Create<List<CardTransaction>>());

            // Act
            var actual = await _target.GetCardTransactionsAsync(request);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Valid_Input_When_GetCardTransactionsAsync_Has_No_Result_Should_Be_204()
        {
            // Arrange
            var request = Fixture.Create<GetCardTransactionsQuery>();
            MockMediator.Setup(x => x.Send(request, default))
                .ReturnsAsync(new List<CardTransaction>());

            // Act
            var actual = await _target.GetCardTransactionsAsync(request);

            // Assert
            actual.GetType().Should().Be<NoContentResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_GetCardTransactionsAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<GetCardTransactionsQuery>();
            MockMediator.Setup(x => x.Send(request, default))
                .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetCardTransactionsAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }
    }
}
