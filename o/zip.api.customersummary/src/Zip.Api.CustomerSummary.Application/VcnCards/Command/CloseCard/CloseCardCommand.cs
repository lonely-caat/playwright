using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.CloseCard
{
    public class CloseCardCommand : IRequest
    {
        public CloseCardCommand(Guid cardId)
        {
            CardId = cardId;
        }

        public CloseCardCommand()
        {
        }

        [FromRoute(Name = "cardId")]
        public Guid CardId { get; set; }
    }
}
