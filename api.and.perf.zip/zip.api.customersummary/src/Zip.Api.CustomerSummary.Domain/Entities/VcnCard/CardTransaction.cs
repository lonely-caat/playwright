using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.VcnCard
{
    public class CardTransaction
    {
        public string Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string Merchant { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string Token { get; set; }
        public string DeviceName { get; set; }
        public decimal? Amount { get; set; }
        public string Memo { get; set; }
        public string NetworkReferenceId { get; set; }
        public string Metadata { get; set; }
    }
}