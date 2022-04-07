using MediatR;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.UpdateInstallmentsEnabled
{
    public class UpdateInstallmentsEnabledCommand : IRequest<AccountResponse>
    {
        public long AccountId { get; set; }

        public long AccountTypeId { get; set; }

        public bool IsInstallmentsEnabled { get; set; }
    }
}
