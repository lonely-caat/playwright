using Polly.Retry;

namespace Zip.Api.CustomerSummary.Persistence.Interfaces
{
    internal interface IDbPolicyProvider
    {
        AsyncRetryPolicy GetRetryPolicy();
    }
}
