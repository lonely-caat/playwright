using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Controllers;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Transaction.Command.CreateVcnTransaction;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Controllers
{
    public class TransactionControllerTests : CommonTestsFixture
    {
        private readonly TransactionController _target;

        public TransactionControllerTests()
        {
            _target = new TransactionController(MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Request_CreateVcnTransactionAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<CreateVcnTransactionCommand>();
            var response = Fixture.Create<CreateVcnTransactionResponse>();
            MockMediator.Setup(x => x.Send(request, CancellationToken))
                        .ReturnsAsync(response);

            // Act
            var actual = await _target.CreateVcnTransactionAsync(request);

            // Assert
            actual.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_CreateVcnTransactionAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<CreateVcnTransactionCommand>();

            MockMediator.Setup(x => x.Send(request, CancellationToken))
                        .ThrowsAsync(new Exception());

            // Act
            Func<Task> func = async () => { await _target.CreateVcnTransactionAsync(request); };

            // Assert
            await func.Should().ThrowAsync<Exception>();
        }
    }
}