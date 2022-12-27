using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces
{
    public interface IVcnCardsApiProxy
    {
        [Get("/internal/cards/{cardId}")]
        Task<HttpResponseMessage> GetCardAsync(Guid cardId, CancellationToken cancellationToken);

        [Get("/internal/cards?externalId={externalId}")]
        Task<HttpResponseMessage> GetCardByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

        [Get("/internal/cards?customerId={customerId}")]
        Task<HttpResponseMessage> GetCardsAsync(Guid customerId, long? accountId, CancellationToken cancellationToken);

        [Put("/cards/{cardId}/block")]
        Task<HttpResponseMessage> BlockCardAsync([Sidebar("Customer-Id")] string customerId, Guid cardId, CancellationToken cancellationToken);

        [Put("/cards/{cardId}/unblock")]
        Task<HttpResponseMessage> UnblockCardAsync([Sidebar("Customer-Id")] string customerId, Guid cardId, CancellationToken cancellationToken);

        [Put("/cards/{cardId}/close")]
        Task<HttpResponseMessage> CloseCardAsync([Sidebar("Customer-Id")] string customerId, Guid cardId, CancellationToken cancellationToken);

        [Post("/internal/digitalwallet/tokentransition")]
        Task<HttpResponseMessage> SendTokenTransitionRequestAsync(TokenTransitionRequest request, CancellationToken cancellationToken);
    }
}
