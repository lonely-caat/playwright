using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Controllers;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Products.Command.CreateOpenLoopProduct;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Controllers
{
    public class OpenLoopProductsControllerTests : CommonTestsFixture
    {
        private readonly OpenLoopProductController _target;

        public OpenLoopProductsControllerTests()
        {
            _target = new OpenLoopProductController(MockMediator.Object);
        }

        [Fact]
        public async Task Given_Valid_Openloop_Product_For_Creation_Must_Return_Created201_Response()
        {
            var request = Fixture.Create<CreateOpenLoopProductCommand>();

            var expectedResponse = Fixture.Create<CreateOpenLoopProductResponse>();

            MockMediator.Setup(x => x.Send(request, CancellationToken))
                        .ReturnsAsync(expectedResponse);

            var actualResponse = await _target.CreateOpenLoopProductAsync(request);

            actualResponse.Should().BeOfType<CreatedResult>();
        }
    }
}
