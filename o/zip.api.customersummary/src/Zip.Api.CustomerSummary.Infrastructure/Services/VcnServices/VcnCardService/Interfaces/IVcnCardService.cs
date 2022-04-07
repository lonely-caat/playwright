using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces
{
    public interface IVcnCardService
    {
        Task<Card> GetCardAsync(Guid cardId, CancellationToken cancellationToken);

        Task<Card> GetCardByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

        Task<RootCards> GetCardsAsync(Guid customerId, long? accountId, CancellationToken cancellationToken);

        Task BlockCardAsync(Guid cardId, CancellationToken cancellationToken);
        
        Task UnblockCardAsync(Guid cardId, CancellationToken cancellationToken);
        
        Task CloseCardAsync(Guid cardId, CancellationToken cancellationToken);
        
        Task SendTokenTransitionRequestAsync(TokenTransitionRequest request, CancellationToken cancellationToken);
    }
}
