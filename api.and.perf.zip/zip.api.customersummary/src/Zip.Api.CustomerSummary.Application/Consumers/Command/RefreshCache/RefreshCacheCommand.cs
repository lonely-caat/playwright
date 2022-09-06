using MediatR;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.RefreshCache
{
    public class RefreshCacheCommand : IRequest
    {
        public long ConsumerId { get; set; }
    }
}
