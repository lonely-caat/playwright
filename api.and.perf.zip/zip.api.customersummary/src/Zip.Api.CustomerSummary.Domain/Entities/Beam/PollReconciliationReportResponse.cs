using System;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Domain.Entities.Beam
{
    [ExcludeFromCodeCoverage]
    public class PollReconciliationReportResponse
    {
        public Guid Uuid { get; set; }

        public bool Complete { get; set; }

        public string Url { get; set; }
    }
}
