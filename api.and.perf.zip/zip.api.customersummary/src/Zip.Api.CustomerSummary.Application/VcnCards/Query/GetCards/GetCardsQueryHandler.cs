using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCards
{
    public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, RootCards>
    {
        private readonly IVcnCardService _vcnCardService;

        public GetCardsQueryHandler(IVcnCardService vcnCardService)
        {
            _vcnCardService = vcnCardService ?? throw new ArgumentNullException(nameof(vcnCardService));
        }

        public async Task<RootCards> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            Log.Information("{class} :: {action} : {message}",
                            nameof(GetCardsQueryHandler),
                            nameof(Handle),
                            $"Start getting cards for Request::{request.ToJsonString()}");

            var rv = await _vcnCardService.GetCardsAsync(request.CustomerId, request.AccountId, cancellationToken);
            var emlCardsCount = rv.Cards.RemoveAll(x => string.Equals(x.Type, nameof(VcnCardType.ONLINE), StringComparison.OrdinalIgnoreCase));
            
            rv.Cards = rv.Cards.OrderByDescending(x => x.CreatedOn).ToList();

            Log.Information("{class} :: {action} : {message}",
                            nameof(GetCardsQueryHandler),
                            nameof(Handle),
                            $"Successfully retrieved cards for Request::{request.ToJsonString()} and filtered out {emlCardsCount} EML Cards");

            return rv;
        }
    }
}
