using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCard
{
    public class GetCardQuery : IRequest<Card>
    {
        public GetCardQuery(Guid cardId, Guid externalId)
        {
            CardId = cardId;
            ExternalId = externalId;
        }

        public GetCardQuery()
        {
        }

        [FromQuery(Name = "cardId")]
        public Guid CardId { get; set; }

        [FromQuery(Name = "externalId")]
        public Guid ExternalId { get; set; }
    }
}
