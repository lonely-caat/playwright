using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Domain.Entities.Mfa
{
    [ExcludeFromCodeCoverage]
    public class MfaSmsDataResponse
    {
        public long Status { get; set; }
        public bool IsSuccess { get; set; }
        public MfaSmsData Data { get; set; }
        public List<MfaError> Errors { get; set; }
    }
}