using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces
{
    public interface IPaymentWebhookApiProxy
    {
        [Get("/internal/transactions?externalId={externalCardId}")]
        Task<HttpResponseMessage> GetCardTransactionsAsync(Guid externalCardId);

        [Get("/internal/transactions?nrid={networkReferenceId}")]
        Task<HttpResponseMessage> GetTransactionsByNetworkReferenceIdAsync(string networkReferenceId);
    }
}
