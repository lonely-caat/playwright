using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetAttributes;
using FluentAssertions;
using MediatR;
using Zip.Api.CustomerSummary.Application.Consumers.Command.SetConsumerAttributes;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumerAttributes;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Attribute = Zip.Api.CustomerSummary.Domain.Entities.Consumers.Attribute;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class AttributesControllerTests : CommonTestsFixture
    {
        private readonly AttributesController _target;

        public AttributesControllerTests()
        {
            _target = new AttributesController(MockMediator.Object);
        }

        [Fact]
        public async Task GetAttributesAsync_Should_Be_200()
        {
            // Arrange
            var expect = Fixture.CreateMany<Attribute>();

            MockMediator.Setup(x => x.Send(It.IsAny<GetAttributesQuery>(), default))
                        .ReturnsAsync(expect);

            // Act
            var actual = await _target.GetAttributesAsync();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_GetAttributesAsync_Should_Be_500()
        {
            // Arrange
            MockMediator.Setup(x => x.Send(It.IsAny<GetAttributesQuery>(), default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetAttributesAsync();

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_ConsumerId_When_GetConsumerAttributesAsync_Should_Be_200()
        {
            // Arrange
            const long consumerId = 1;
            var expect = Fixture.CreateMany<ConsumerAttributeDto>();
            MockMediator.Setup(x => x.Send(It.Is<GetConsumerAttributesQuery>(y => y.ConsumerId == consumerId), default))
                        .ReturnsAsync(expect);

            // Act
            var actual = await _target.GetConsumerAttributesAsync(consumerId);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Invalid_ConsumerId_When_GetConsumerAttributesAsync_Should_Be_400()
        {
            // Arrange
            const long consumerId = -1;

            // Act
            var actual = await _target.GetConsumerAttributesAsync(consumerId);

            // Assert
            actual.GetType().Should().Be<BadRequestObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_GetConsumerAttributesAsync_Should_Be_200()
        {
            // Arrange
            MockMediator.Setup(x => x.Send(It.IsAny<GetConsumerAttributesQuery>(), default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetConsumerAttributesAsync(1);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_SetConsumerAttributesAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<SetConsumerAttributesCommand>();
            
            MockMediator.Setup(x => x.Send(It.IsAny<SetConsumerAttributesCommand>(), default))
                        .ReturnsAsync(new Unit());

            // Act
            var actual = await _target.SetConsumerAttributesAsync(request);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_SetConsumerAttributesAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<SetConsumerAttributesCommand>();
            
            MockMediator.Setup(x => x.Send(It.IsAny<SetConsumerAttributesCommand>(), default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.SetConsumerAttributesAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }
    }
}
