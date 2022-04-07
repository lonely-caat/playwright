using MediatR;

namespace Zip.Api.CustomerSummary.Application.Orders.Command
{
    public class CancelOrderInstallmentsCommand : IRequest<Unit>
    {
        public long AccountId { get; set; }

        public long OrderId { get; set; }
    }
}
