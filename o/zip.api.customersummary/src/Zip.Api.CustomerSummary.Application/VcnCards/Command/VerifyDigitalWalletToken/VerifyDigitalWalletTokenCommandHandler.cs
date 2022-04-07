using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.VerifyDigitalWalletToken
{
    public class VerifyDigitalWalletTokenCommandHandler : IRequestHandler<VerifyDigitalWalletTokenCommand>
    {
        private readonly IVcnCardService _vcnCardService;

        private readonly IMapper _mapper;
        
        public VerifyDigitalWalletTokenCommandHandler(IVcnCardService vcnCardService, IMapper mapper)
        {
            _vcnCardService = vcnCardService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(VerifyDigitalWalletTokenCommand request, CancellationToken cancellationToken)
        {
            Log.Information("{class} :: {action} : {message}",
                            nameof(VerifyDigitalWalletTokenCommandHandler),
                            nameof(Handle),
                            $"Start verifying king DigitalWalletToken::{request.DigitalWalletToken}");

            var tokenTransitionRequest = _mapper.Map<TokenTransitionRequest>(request);

            await _vcnCardService.SendTokenTransitionRequestAsync(tokenTransitionRequest, cancellationToken);

            Log.Information("{class} :: {action} : {message}",
                            nameof(VerifyDigitalWalletTokenCommandHandler),
                            nameof(Handle),
                            $"Successfully verified DigitalWalletToken::{request.DigitalWalletToken}");

            return Unit.Value;
        }
    }
}
