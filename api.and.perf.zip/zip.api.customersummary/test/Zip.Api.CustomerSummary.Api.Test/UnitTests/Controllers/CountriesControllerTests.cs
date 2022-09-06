using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Application.Countries.Query.GetCountries;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class CountriesControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public CountriesControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CountriesController(null));
        }

        [Fact]
        public async Task Given_NoCountriesFound_ShouldReturn_NotFound()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetCountriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as List<Country>);

            var controller = new CountriesController(_mediator.Object);
            var result = await controller.GetAsync();

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Given_CountriesFound_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetCountriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Country>() { new Country(), new Country() });

            var controller = new CountriesController(_mediator.Object);
            var result = await controller.GetAsync();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_Get_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetCountriesQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());


            var controller = new CountriesController(_mediator.Object);
            var result = await controller.GetAsync();

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }
    }
}
