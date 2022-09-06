using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using FluentAssertions;
using Zip.Api.CustomerSummary.Application.Communications.Query.GetEmailsSent;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class MessagesControllerTests : CommonTestsFixture
    {
        private readonly MessagesController _target;

        public MessagesControllerTests()
        {
            _target = new MessagesController(MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Input_GetEmailsSentAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<GetEmailsSentQuery>();
            var expect = Fixture.CreateMany<EmailSent>();
            
            MockMediator.Setup(x => x.Send(request, default))
                        .ReturnsAsync(expect);

            // Act
            var actual = await _target.GetEmailsSentAsync(request);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_GetEmailsSentAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<GetEmailsSentQuery>();
            
            MockMediator.Setup(x => x.Send(request, default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.GetEmailsSentAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }
    }
}
