using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockDigitalWalletToken
{
    public class UnblockDigitalWalletTokenCommand : IRequest
    {
        public UnblockDigitalWalletTokenCommand(Guid token)
        {
            DigitalWalletToken = token;
        }

        public UnblockDigitalWalletTokenCommand()
        {
        }

        [FromRoute(Name = "digitalWalletToken")]
        public Guid DigitalWalletToken { get; set; }
    }
}
