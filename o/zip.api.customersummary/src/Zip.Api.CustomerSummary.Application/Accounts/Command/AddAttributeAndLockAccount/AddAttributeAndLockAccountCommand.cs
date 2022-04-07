using MediatR;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.AddAttributeAndLockAccount
{
    public class AddAttributeAndLockAccountCommand : IRequest<Unit>
    {
        public long AccountId { get; set; }

        public string ChangedBy { get; set; }

        public long ConsumerId { get; set; }

        public string Reason { get; set; }

        public string Attribute { get; set; }

    }
}
