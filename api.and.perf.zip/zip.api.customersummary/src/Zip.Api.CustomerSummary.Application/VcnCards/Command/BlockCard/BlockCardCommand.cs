using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockCard
{
    public class BlockCardCommand : IRequest
    {
        public BlockCardCommand(Guid cardId)
        {
            CardId = cardId;
        }

        public BlockCardCommand()
        {
        }

        [FromRoute(Name = "cardId")]
        public Guid CardId { get; set; }
    }
}
