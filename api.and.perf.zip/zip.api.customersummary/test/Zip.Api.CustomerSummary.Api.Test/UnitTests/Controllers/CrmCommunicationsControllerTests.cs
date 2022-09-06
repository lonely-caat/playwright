using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using FluentAssertions;
using Zip.Api.CustomerSummary.Application.Communications.Query.GetCrmCommunications;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Application.Communications.Command.InsertCrmCommunication;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class CrmCommunicationsControllerTests : CommonTestsFixture
    {
        private readonly CrmCommunicationsController _target;

        public CrmCommunicationsControllerTests()
        {
            _target = new CrmCommunicationsController(MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Input_GetAsync_Should_Be_200()
        {
            // Arrange
            var expect = Fixture.CreateMany<MessageLogDto>();

            MockMediator.Setup(x => x.Send(It.IsAny<GetCrmCommunicationsQuery>(), default))
                        .ReturnsAsync(expect);

            // Act
            var actual = await _target.GetAsync(1);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }
        
        [Fact]
        public async Task Given_Invalid_ConsumerId_When_GetAsync_Should_Be_400()
        {
            // Arrange
            const long consumerId = -1;

            // Act
            var actual = await _target.GetAsync(consumerId);

            // Assert
            actual.GetType().Should().Be<BadRequestObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_GetAsync_Should_Be_500()
        {
            // Arrange
            MockMediator.Setup(x => x.Send(It.IsAny<GetCrmCommunicationsQuery>(), default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetAsync(1);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Given_Valid_Input_InsertCommunicationCommentAsync_Should_Be_200()
        {
            // Arrange
            var settings = Fixture.Create<MessageLogSettings>();
            var mockInput = Fixture.Create<InsertCrmCommunicationCommand>();

            MockMediator.Setup(x => x.Send(It.IsAny<InsertCrmCommunicationCommand>(), default)).ReturnsAsync(true);

            // Act
            var response = await _target.InsertCommunicationCommentAsync(mockInput);

            // Assert
            response.GetType().Should().Be<OkObjectResult>();
            var objectResponse = response as OkObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Valid_Input_InsertCommunicationCommentAsync_Should_Be_500()
        {
            // Arrange
            var settings = Fixture.Create<MessageLogSettings>();
            var mockInput = Fixture.Create<InsertCrmCommunicationCommand>();

            MockMediator.Setup(x => x.Send(It.IsAny<InsertCrmCommunicationCommand>(), default)).ThrowsAsync(new Exception());

            // Act
            var response = await _target.InsertCommunicationCommentAsync(mockInput);

            // Assert
            response.GetType().Should().Be<ObjectResult>();
            var objectResponse = response as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }
    }
}
