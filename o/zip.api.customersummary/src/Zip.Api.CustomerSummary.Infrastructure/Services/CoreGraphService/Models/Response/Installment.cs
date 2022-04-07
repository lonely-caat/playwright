using System;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response
{
    public class Installment
    {
        public long OrderId { get; set; }

        public string DisplayName { get; set; }

        public decimal Amount { get; set; }

        public DateTime date { get; set; }

        public string Status { get; set; }
    }
}
