using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetOrderActivities;
using Zip.Api.CustomerSummary.Application.Transactions.Query.GetTransactionHistory;
using Zip.Api.CustomerSummary.Application.Transactions.Query.GetVcnTransactions;
using Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class TransactionsControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public TransactionsControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TransactionsController(null));
        }

        [Fact]
        public async Task Given_ConsumerIdInvalid_ShouldReturn_BadRequest()
        {
            var controller = new TransactionsController(_mediator.Object);
            var ar = await controller.FindTransactionHistoryAsync(0, null, null);

            Assert.IsType<BadRequestObjectResult>(ar);
        }

        [Fact]
        public async Task Given_NoResult_ShouldReturn_NoContent()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetTransactionHistoryQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(null as IEnumerable<TransactionHistory>);

            var controller = new TransactionsController(_mediator.Object);
            var ar = await controller.FindTransactionHistoryAsync(201, null, null);

            Assert.IsType<NoContentResult>(ar);
        }

        [Fact]
        public async Task Given_Result_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetTransactionHistoryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<TransactionHistory> { new TransactionHistory() });

            var controller = new TransactionsController(_mediator.Object);
            var ar = await controller.FindTransactionHistoryAsync(201, null, null);

            Assert.IsType<OkObjectResult>(ar);
        }

        [Fact]
        public async Task When_Error_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetTransactionHistoryQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new TransactionsController(_mediator.Object);
            var ar = await controller.FindTransactionHistoryAsync(201, null, null);

            Assert.IsType<ObjectResult>(ar);
            Assert.Equal(500, (ar as ObjectResult)?.StatusCode);
        }

        [Fact]
        public async Task Given_InvalidConsumerId_OrderActivity_ShouldReturn_BadRequest()
        {
            var controller = new TransactionsController(_mediator.Object);
            var ar = await controller.GetOrderActivityAsync(0, null, null);

            Assert.IsType<BadRequestObjectResult>(ar);
        }

        [Fact]
        public async Task Given_NoResult_OrderActivity_ShouldReturn_NoContent()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetOrderActivitiesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as IEnumerable<OrderActivityDto>);

            var controller = new TransactionsController(_mediator.Object);
            var ar = await controller.GetOrderActivityAsync(305, null, null);

            Assert.IsType<NoContentResult>(ar);
        }

        [Fact]
        public async Task Given_Result_OrderActivity_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetOrderActivitiesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<OrderActivityDto>() {
                new OrderActivityDto() });

            var controller = new TransactionsController(_mediator.Object);
            var ar = await controller.GetOrderActivityAsync(305, null, null);

            Assert.IsType<OkObjectResult>(ar);
        }

        [Fact]
        public async Task When_Error_OrderActivity_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetOrderActivitiesQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new TransactionsController(_mediator.Object);
            var ar = await controller.GetOrderActivityAsync(305, null, null);

            Assert.IsType<ObjectResult>(ar);
            Assert.Equal(500, (ar as ObjectResult)?.StatusCode);
        }

        [Fact]
        public async Task Given_Result_VcnTransactions_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetVcnTransactionsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<CardTransaction>() { new CardTransaction() });

            var controller = new TransactionsController(_mediator.Object);
            var ar = await controller.GetVcnTransactionsAsync(new GetVcnTransactionsQuery("2345678"));

            Assert.IsType<OkObjectResult>(ar);
        }
    }
}
