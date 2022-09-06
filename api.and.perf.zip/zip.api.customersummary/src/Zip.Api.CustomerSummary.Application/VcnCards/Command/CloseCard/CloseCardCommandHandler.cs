using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.CloseCard
{
    public class CloseCardCommandHandler : IRequestHandler<CloseCardCommand>
    {
        private readonly IVcnCardService _vcnCardService;

        public CloseCardCommandHandler(IVcnCardService vcnCardService)
        {
            _vcnCardService = vcnCardService;
        }

        public async Task<Unit> Handle(CloseCardCommand request, CancellationToken cancellationToken)
        {
            Log.Information("{class} :: {action} : {message}",
                            nameof(CloseCardCommandHandler),
                            nameof(Handle),
                            $"Start closing card of cardId::{request.CardId}");

            await _vcnCardService.CloseCardAsync(request.CardId, cancellationToken);

            Log.Information("{class} :: {action} : {message}",
                            nameof(CloseCardCommandHandler),
                            nameof(Handle),
                            $"Successfully closed card of cardId::{request.CardId}");

            return Unit.Value;
        }
    }
}
