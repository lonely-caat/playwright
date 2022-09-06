using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Accounts
{
    public class Account
    {
        public long Id { get; set; }

        public long ConsumerId { get; set; }

        public DateTime TimeStamp { get; set; }

        public DateTime? StatementDate { get; set; }

        public long AccountTypeId { get; set; }

        public AccountType AccountType { get; set; }

        public AccountStatus AccountStatus { get; set; }

        public bool IsActive =>
                AccountStatus == AccountStatus.Active ||
                AccountStatus == AccountStatus.Operational ||
                AccountStatus == AccountStatus.Locked;
    }
}
