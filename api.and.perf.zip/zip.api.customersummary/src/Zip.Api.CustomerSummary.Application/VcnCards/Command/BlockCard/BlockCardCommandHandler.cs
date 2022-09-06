using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCards;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;

namespace Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockCard
{
    public class BlockCardCommandHandler : IRequestHandler<BlockCardCommand>
    {
        private readonly IVcnCardService _vcnCardService;

        public BlockCardCommandHandler(IVcnCardService vcnCardService)
        {
            _vcnCardService = vcnCardService;
        }

        public async Task<Unit> Handle(BlockCardCommand request, CancellationToken cancellationToken)
        {
            Log.Information("{class} :: {action} : {message}",
                            nameof(GetCardsQueryHandler),
                            nameof(Handle),
                            $"Start blocking card of cardId::{request.CardId}");

            await _vcnCardService.BlockCardAsync(request.CardId, cancellationToken);

            Log.Information("{class} :: {action} : {message}",
                            nameof(GetCardsQueryHandler),
                            nameof(Handle),
                            $"Successfully blocked card of cardId::{request.CardId}");

            return Unit.Value;
        }
    }
}
