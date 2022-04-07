using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Domain.Entities.Beam
{
    public class RewardActivityResponse
    {
        public long Count { get; set; }

        public long TotalCount { get; set; }

        public bool HasMore { get; set; }

        public IEnumerable<RewardActivity> Elements { get; set; }
    }
}
