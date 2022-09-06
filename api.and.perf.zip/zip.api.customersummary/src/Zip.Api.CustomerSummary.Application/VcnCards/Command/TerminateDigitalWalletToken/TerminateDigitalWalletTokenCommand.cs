using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.TerminateDigitalWalletToken
{
    public class TerminateDigitalWalletTokenCommand : IRequest
    {
        public TerminateDigitalWalletTokenCommand(Guid token)
        {
            DigitalWalletToken = token;
        }

        public TerminateDigitalWalletTokenCommand()
        {
        }

        [FromRoute(Name = "digitalWalletToken")]
        public Guid DigitalWalletToken { get; set; }

    }
}
