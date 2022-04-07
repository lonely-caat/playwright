using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Application.Statements.Command.GenerateStatement;
using Zip.Api.CustomerSummary.Application.Statements.Models;
using Zip.Api.CustomerSummary.Application.Statements.Query.GetStatementDates;
using Zip.Api.CustomerSummary.Domain.Entities.Statement;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class StatementsControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public StatementsControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new StatementsController(null));
        }

        [Fact]
        public async Task Given_GeneratedStatement_Should_Return_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GenerateStatementCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GenerateStatementResponse(true));

            var controller = new StatementsController(_mediator.Object);
            var ar = await controller.GenerateStatementAsync(new GenerateStatementCommand());

            Assert.IsType<OkObjectResult>(ar);
        }

        [Fact]
        public async Task Given_GenerateStatement_When_Error_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GenerateStatementCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new StatementsController(_mediator.Object);
            var ar = await controller.GenerateStatementAsync(new GenerateStatementCommand());

            Assert.Equal(500, ((ObjectResult)ar).StatusCode);
        }

        [Fact]
        public async Task Given_InvalidAccountId_ShouldReturn_BadRequest()
        {
            var controller = new StatementsController(_mediator.Object);
            var ar = await controller.GetAvailableStatementDatesAsync(0);

            Assert.IsType<BadRequestObjectResult>(ar);
        }

        [Fact]
        public async Task Given_NoStatementDates_ShouldReturn_NotFound()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetStatementDatesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as List<StatementDate>);

            var controller = new StatementsController(_mediator.Object);
            var ar = await controller.GetAvailableStatementDatesAsync(10);

            Assert.IsType<NoContentResult>(ar);
        }

        [Fact]
        public async Task Given_StatementDates_ShouldReturn()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetStatementDatesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<StatementDate>()
                {
                    new StatementDate(new DateTime(2010,1,1), new DateTime(2010,2,2))
                });

            var controller = new StatementsController(_mediator.Object);
            var ar = await controller.GetAvailableStatementDatesAsync(10);

            Assert.IsType<OkObjectResult>(ar);
        }

        [Fact]
        public async Task Given_Error_WhenCall_GetAvailableStatementDates_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetStatementDatesQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new StatementsController(_mediator.Object);
            var ar = await controller.GetAvailableStatementDatesAsync(10);

            Assert.Equal(500, ((ObjectResult)ar).StatusCode);
        }
    }
}
