using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Domain.Entities.Mfa
{
    [ExcludeFromCodeCoverage]
    public class MfaError
    {
        public long Code { get; set; }
        public string Message { get; set; }
    }
}