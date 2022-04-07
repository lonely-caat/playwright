using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.VerifyDigitalWalletToken
{
    public class VerifyDigitalWalletTokenCommand : IRequest
    {
        public VerifyDigitalWalletTokenCommand(Guid token)
        {
            DigitalWalletToken = token;
        }

        public VerifyDigitalWalletTokenCommand()
        {
        }

        [FromRoute(Name = "digitalWalletToken")]
        public Guid DigitalWalletToken { get; set; }
    }
}
