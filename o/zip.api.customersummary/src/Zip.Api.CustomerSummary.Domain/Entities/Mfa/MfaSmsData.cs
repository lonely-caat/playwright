using System;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Domain.Entities.Mfa
{
    [ExcludeFromCodeCoverage]
    public class MfaSmsData
    {
        public string ActiveSmsCode { get; set; }
        public DateTime SmsCodeIssueDateTime { get; set; }
        public bool IsSmsSendLimitExceeded { get; set; }
        public bool IsVerificationLimitExceeded { get; set; }
    }
}