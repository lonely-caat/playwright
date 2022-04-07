using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces
{
    public interface IPaymentWebhookService
    {
        Task<IEnumerable<CardTransaction>> GetCardTransactionsAsync(Guid cardId, CancellationToken cancellationToken);

        Task<IEnumerable<CardTransaction>> GetTransactionsByNetworkReferenceIdAsync(string networkReferenceId, CancellationToken cancellationToken);
    }
}
