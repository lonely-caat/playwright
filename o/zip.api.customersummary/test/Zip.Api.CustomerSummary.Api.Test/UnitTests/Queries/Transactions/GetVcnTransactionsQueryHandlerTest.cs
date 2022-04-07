using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Transactions.Query.GetVcnTransactions;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Transactions
{
    public class GetVcnTransactionsQueryHandlerTest
    {
        private readonly Mock<IPaymentWebhookService> _service;

        public GetVcnTransactionsQueryHandlerTest()
        {
            _service = new Mock<IPaymentWebhookService>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GetVcnTransactionsQueryHandler(null));
        }

        [Fact]
        public async Task Should_return()
        {
            _service.Setup(x => x.GetTransactionsByNetworkReferenceIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<CardTransaction>
                {
                    new CardTransaction
                    {
                        NetworkReferenceId = "NetworkReferenceId",
                    },
                    new CardTransaction
                    {
                        NetworkReferenceId = "6100383747"
                    }
                });

            var handler = new GetVcnTransactionsQueryHandler(_service.Object);
            var result = await handler.Handle(new GetVcnTransactionsQuery("NetworkReferenceId"), CancellationToken.None);

            Assert.Equal(2, result.Count());
        }
    }
}
