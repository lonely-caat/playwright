using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockDigitalWalletToken
{
    public class UnblockDigitalWalletTokenCommandHandler : IRequestHandler<UnblockDigitalWalletTokenCommand>
    {
        private readonly IVcnCardService _vcnCardService;

        private readonly IMapper _mapper;
        
        public UnblockDigitalWalletTokenCommandHandler(IVcnCardService vcnCardService, IMapper mapper)
        {
            _vcnCardService = vcnCardService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UnblockDigitalWalletTokenCommand request, CancellationToken cancellationToken)
        {
            Log.Information("{class} :: {action} : {message}",
                            nameof(UnblockDigitalWalletTokenCommandHandler),
                            nameof(Handle),
                            $"Start unblocking DigitalWalletToken::{request.DigitalWalletToken}");

            var tokenTransitionRequest = _mapper.Map<TokenTransitionRequest>(request);

            await _vcnCardService.SendTokenTransitionRequestAsync(tokenTransitionRequest, cancellationToken);

            Log.Information("{class} :: {action} : {message}",
                            nameof(UnblockDigitalWalletTokenCommandHandler),
                            nameof(Handle),
                            $"Successfully unblocked DigitalWalletToken::{request.DigitalWalletToken}");

            return Unit.Value;
        }
    }
}
