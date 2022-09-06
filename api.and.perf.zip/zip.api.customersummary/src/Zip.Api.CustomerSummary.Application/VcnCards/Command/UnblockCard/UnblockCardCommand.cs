using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockCard
{
    public class UnblockCardCommand : IRequest
    {
        public UnblockCardCommand(Guid cardId)
        {
            CardId = cardId;
        }

        public UnblockCardCommand()
        {
        }
        
        [FromRoute(Name = "cardId")]
        public Guid CardId { get; set; }
    }
}
