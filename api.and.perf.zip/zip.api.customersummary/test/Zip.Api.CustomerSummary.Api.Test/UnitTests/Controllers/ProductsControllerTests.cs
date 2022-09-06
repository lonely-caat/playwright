using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Application.Products.Query.GetProducts;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public ProductsControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProductsController(null));
        }

        [Fact]
        public async Task Given_ProductsFound_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Product>() { 
                
                new Product(),
                new Product()});

            var controller = new ProductsController(_mediator.Object);
            var ar = await controller.GetAsync();

            Assert.IsType<OkObjectResult>(ar);
        }

        [Fact]
        public async Task Given_NoProductsFound_ShouldReturn_NoContent()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
    .ReturnsAsync(null as List<Product>);

            var controller = new ProductsController(_mediator.Object);
            var ar = await controller.GetAsync();

            Assert.IsType<NoContentResult>(ar);
        }
    }
}
