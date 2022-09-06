using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Api.Test.Common;
using FluentAssertions;
using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class InternalAccountControllerTests : CommonTestsFixture
    {
        private readonly InternalAccountController _target;

        public InternalAccountControllerTests()
        {
            _target = new InternalAccountController(MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Input_LockAccountAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Build<LockAccountCommand>()
                                 .With(x => x.ConsumerId, 1)
                                 .Create();

            MockMediator.Setup(x => x.Send(request, default))
                        .ReturnsAsync(new Unit());

            // Act
            var actual = await _target.LockAccountAsync(request);

            // Assert
            actual.Should().BeOfType<OkResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_When_LockAccountAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Build<LockAccountCommand>()
                                 .With(x => x.ConsumerId, 1)
                                 .Create();
            
            MockMediator.Setup(x => x.Send(It.IsAny<LockAccountCommand>(), default))
                        .ThrowsAsync(new Exception());

            // Act
            var actual = await _target.LockAccountAsync(request);

            // Assert
            actual.GetType().Should().Be<ObjectResult>();
            var objectResponse = actual as ObjectResult;
            objectResponse?.StatusCode.Should().Be(500);
        }
    }
}
