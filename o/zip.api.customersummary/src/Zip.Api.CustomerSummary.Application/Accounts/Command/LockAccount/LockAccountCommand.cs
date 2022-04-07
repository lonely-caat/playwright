using MediatR;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount
{
    public class LockAccountCommand : IRequest
    {
        public long ConsumerId { get; set; }

        public long AccountId { get; set; }

        public string Reason { get; set; }

        public string ChangedBy { get; set; }
    }
}
