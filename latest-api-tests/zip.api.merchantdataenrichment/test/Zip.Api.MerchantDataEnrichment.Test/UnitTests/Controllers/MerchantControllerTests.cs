using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Controllers;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Command.GetOrUpsertMerchantDetail;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Controllers
{
    public class MerchantControllerTests : CommonTestsFixture
    {
        private readonly MerchantController _target;

        public MerchantControllerTests()
        {
            _target = new MerchantController(MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Request_GetOrUpsertMerchantDetailAsync_Should_Be_200()
        {
            // Arrange
            var request = Fixture.Create<GetOrUpsertMerchantDetailCommand>();
            var response = Fixture.Create<GetOrUpsertMerchantDetailResponse>();
            MockMediator.Setup(x => x.Send(request, CancellationToken))
                        .ReturnsAsync(response);

            // Act
            var actual = await _target.GetOrUpsertMerchantDetailAsync(request);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Given_Handler_Throws_Exception_CreateVcnTransactionAsync_Should_Be_500()
        {
            // Arrange
            var request = Fixture.Create<GetOrUpsertMerchantDetailCommand>();

            MockMediator.Setup(x => x.Send(request, CancellationToken))
                        .ThrowsAsync(new Exception());

            // Act
            Func<Task> func = async () => { await _target.GetOrUpsertMerchantDetailAsync(request); };

            // Assert
            await func.Should().ThrowAsync<Exception>();
        }
    }
}
