using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCard
{
    public class GetCardQueryHandler : IRequestHandler<GetCardQuery, Card>
    {
        private readonly IVcnCardService _vcnCardService;

        public GetCardQueryHandler(IVcnCardService vcnCardService)
        {
            _vcnCardService = vcnCardService;
        }

        public async Task<Card> Handle(GetCardQuery request, CancellationToken cancellationToken)
        {
            if (request.CardId != Guid.Empty)
            {
                Log.Information("{class} :: {action} : {message}",
                            nameof(GetCardQueryHandler),
                            nameof(Handle),
                            $"Start getting card of CardId::{request.CardId}");

                var rv = await _vcnCardService.GetCardAsync(request.CardId, cancellationToken);

                Log.Information("{class} :: {action} : {message}",
                                nameof(GetCardQueryHandler),
                                nameof(Handle),
                                $"Successfully retrieved card of CardId::{request.CardId}");
                return rv;
            }
            else
            {
                Log.Information("{class} :: {action} : {message}",
                            nameof(GetCardQueryHandler),
                            nameof(Handle),
                            $"Start getting card of ExternalId::{request.ExternalId}");

                var rv = await _vcnCardService.GetCardByExternalIdAsync(request.ExternalId, cancellationToken);

                Log.Information("{class} :: {action} : {message}",
                                nameof(GetCardQueryHandler),
                                nameof(Handle),
                                $"Successfully retrieved card of ExternalId::{request.ExternalId}");
                return rv;
            }
        }
    }
}
