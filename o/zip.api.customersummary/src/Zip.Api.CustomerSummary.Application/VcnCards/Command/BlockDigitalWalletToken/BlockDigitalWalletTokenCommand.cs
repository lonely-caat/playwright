using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockDigitalWalletToken
{
    public class BlockDigitalWalletTokenCommand : IRequest
    {
        public BlockDigitalWalletTokenCommand(Guid token)
        {
            DigitalWalletToken = token;
        }

        public BlockDigitalWalletTokenCommand()
        {
        }

        [FromRoute(Name = "digitalWalletToken")]
        public Guid DigitalWalletToken { get; set; }
    }
}
