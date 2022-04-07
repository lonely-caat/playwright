using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.TerminateDigitalWalletToken
{
    public class TerminateDigitalWalletTokenCommandHandler : IRequestHandler<TerminateDigitalWalletTokenCommand>
    {
        private readonly IVcnCardService _vcnCardService;

        private readonly IMapper _mapper;
        
        public TerminateDigitalWalletTokenCommandHandler(IVcnCardService vcnCardService, IMapper mapper)
        {
            _vcnCardService = vcnCardService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(TerminateDigitalWalletTokenCommand request, CancellationToken cancellationToken)
        {
            Log.Information("{class} :: {action} : {message}",
                            nameof(TerminateDigitalWalletTokenCommandHandler),
                            nameof(Handle),
                            $"Start blocking DigitalWalletToken::{request.DigitalWalletToken}");

            var tokenTransitionRequest = _mapper.Map<TokenTransitionRequest>(request);

            await _vcnCardService.SendTokenTransitionRequestAsync(tokenTransitionRequest, cancellationToken);

            Log.Information("{class} :: {action} : {message}",
                            nameof(TerminateDigitalWalletTokenCommandHandler),
                            nameof(Handle),
                            $"Successfully blocked DigitalWalletToken::{request.DigitalWalletToken}");
            
            return Unit.Value;
        }
    }
}
