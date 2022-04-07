using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Transactions
{
    public class LmsTransaction
    {
        public DateTime TransactionDate { get; set; }

        public string TransactionType { get; set; }

        public string Narrative { get; set; }

        public decimal Amount { get; set; }
    }
}
