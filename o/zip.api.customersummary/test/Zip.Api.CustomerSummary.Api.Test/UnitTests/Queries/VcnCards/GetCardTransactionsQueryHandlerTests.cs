using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCardTransactions;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.VcnCards
{
    public class GetCardTransactionsQueryHandlerTests : CommonTestsFixture
    {
        private readonly Mock<IPaymentWebhookService> _paymentWebhookService;
        private readonly GetCardTransactionsQueryHandler _target;

        public GetCardTransactionsQueryHandlerTests()
        {
            _paymentWebhookService = new Mock<IPaymentWebhookService>();
            _target = new GetCardTransactionsQueryHandler(_paymentWebhookService.Object);
        }

        [Fact]
        public async Task Handle_Should_Invoke_PaymentWebhookService_GetCardTransactionsAsync()
        {
            // Arrange
            var request = Fixture.Create<GetCardTransactionsQuery>();
            _paymentWebhookService.Setup(x => x.GetCardTransactionsAsync(It.IsAny<Guid>(), default))
                .ReturnsAsync(Fixture.Create<List<CardTransaction>>());

            // Act
            await _target.Handle(request, default);

            // Assert
            _paymentWebhookService.Verify(x => x.GetCardTransactionsAsync(It.IsAny<Guid>(), default), Times.Once);
        }
    }
}
