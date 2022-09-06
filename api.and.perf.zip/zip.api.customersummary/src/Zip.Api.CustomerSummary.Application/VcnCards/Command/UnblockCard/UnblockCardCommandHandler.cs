using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockCard
{
    public class UnblockCardCommandHandler : IRequestHandler<UnblockCardCommand>
    {
        private readonly IVcnCardService _vcnCardService;

        public UnblockCardCommandHandler(IVcnCardService vcnCardService)
        {
            _vcnCardService = vcnCardService;
        }

        public async Task<Unit> Handle(UnblockCardCommand request, CancellationToken cancellationToken)
        {
            Log.Information("{class} :: {action} : {message}",
                            nameof(UnblockCardCommandHandler),
                            nameof(Handle),
                            $"Start unblocking card of cardId::{request.CardId}");

            await _vcnCardService.UnblockCardAsync(request.CardId, cancellationToken);

            Log.Information("{class} :: {action} : {message}",
                            nameof(UnblockCardCommandHandler),
                            nameof(Handle),
                            $"Successfully unblocked card of cardId::{request.CardId}");

            return Unit.Value;
        }
    }
}
