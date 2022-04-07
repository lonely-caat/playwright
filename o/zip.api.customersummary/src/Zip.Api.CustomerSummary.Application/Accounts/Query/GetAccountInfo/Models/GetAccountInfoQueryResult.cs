using System;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models
{
    public class GetAccountInfoQueryResult
    {
        public AccountInfo AccountInfo { get; set; } = new AccountInfo();
        
        public AccountType AccountType { get; set; }

        public LmsAccountDto LmsAccount { get; set; }

        public decimal? PayoutQuote { get; set; }

        public DateTime? ArrearsDueDate { get; set; }

        public AccountResponseConfiguration Configuration { get; set; }
    }
}
