using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Statements
{
    public class Statement
    {
        public long Id { get; set; }

        public long ConsumerId { get; set; }

        public string Name { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public DateTime TimeStamp { get; set; }

        public decimal OpeningBalance { get; set; }

        public decimal ClosingBalance { get; set; }

        public decimal InterestFreeBalance { get; set; }

        public decimal AvailableFunds { get; set; }
    }
}
