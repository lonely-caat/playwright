using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Transactions.Query.GetTransactionHistory;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Transactions
{
    public class GetTransactionHistoryHandlerTest
    {
        private readonly Mock<ITransactionHistoryContext> _transactionHistoryContext;

        private readonly Mock<ILogger<GetTransactionHistoryHandler>> _logger;

        private readonly GetTransactionHistoryHandler _target;

        public GetTransactionHistoryHandlerTest()
        {
            _transactionHistoryContext = new Mock<ITransactionHistoryContext>();
            _logger = new Mock<ILogger<GetTransactionHistoryHandler>>();

            _target = new GetTransactionHistoryHandler(_transactionHistoryContext.Object, _logger.Object);
        }

        [Fact]
        public async Task Should_return()
        {
            // Arrange
            _transactionHistoryContext.Setup(x => x.FindByConsumerIdAsync(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<TransactionHistory>
                {
                    new TransactionHistory
                    {
                        OrderId = 322,
                    },
                    new TransactionHistory
                    {
                        OrderId = 39281
                    }
                });

            var result = await _target.Handle(new GetTransactionHistoryQuery(), CancellationToken.None);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task On_StartDate_Null_Should_Default_To_Tomorrow()
        {
            // Arrange
            _transactionHistoryContext
                .Setup(x => x.FindByConsumerIdAsync(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(Enumerable.Empty<TransactionHistory>);

            var tomorrow = DateTime.Now.AddDays(1);

            // Act
            await _target.Handle(new GetTransactionHistoryQuery(), CancellationToken.None);

            // Assert
            _transactionHistoryContext.Verify(x =>
                x.FindByConsumerIdAsync(
                    It.IsAny<long>(),
                    It.IsAny<DateTime>(),
                    It.Is<DateTime>(y => y.Day == tomorrow.Day)),
                Times.Once
            );
        }

        [Fact]
        public async Task On_EndDate_Null_Should_Default_To_SixMonthsAgo()
        {
            // Arrange
            _transactionHistoryContext
                .Setup(x => x.FindByConsumerIdAsync(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(Enumerable.Empty<TransactionHistory>);

            var sixMonthsAgo = DateTime.Now.AddMonths(-6);

            // Act
            await _target.Handle(new GetTransactionHistoryQuery(), CancellationToken.None);

            // Assert
            _transactionHistoryContext.Verify(x =>
                x.FindByConsumerIdAsync(
                    It.IsAny<long>(),
                    It.Is<DateTime>(y => y.Month == sixMonthsAgo.Month),
                    It.IsAny<DateTime>()),
                Times.Once
            );
        }
    }
}
