using System;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Domain.Entities.Beam
{
    [ExcludeFromCodeCoverage]
    public class CustomerDetails
    {
        public Guid CustomerId { get; set; }

        public string Link { get; set; }
        
        public string TestGroup { get; set; }

        public bool Suspended { get; set; }

        public long NumberOfTransaction { get; set; }

        public decimal TotalMoneySpent { get; set; }

        public decimal TotalRewardsEarned { get; set; }

        public decimal TotalRewardsRedeemed { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}
