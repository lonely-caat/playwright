using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockPaymentWebhookService : IPaymentWebhookService
    {
        public Task<IEnumerable<CardTransaction>> GetCardTransactionsAsync(Guid cardId, CancellationToken cancellationToken)
        {
            var id = cardId.ToString();
            if (id.StartsWith("90000000"))
            {
                throw new Exception("test exception");
            }

            var result = new List<CardTransaction>();

            if (!id.StartsWith("10000000"))
            {
                result.Add(new CardTransaction() { Id = cardId.ToString() });
            }

            return Task.FromResult<IEnumerable<CardTransaction>>(result);
        }

        public Task<IEnumerable<CardTransaction>> GetTransactionsByNetworkReferenceIdAsync(string networkReferenceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}