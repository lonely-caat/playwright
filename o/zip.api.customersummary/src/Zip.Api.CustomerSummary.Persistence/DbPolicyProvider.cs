using System;
using System.Data.SqlClient;
using Polly;
using Polly.Retry;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence
{
    public class DbPolicyProvider : IDbPolicyProvider
    {
        public AsyncRetryPolicy GetRetryPolicy()
        {
            return Policy
                    .Handle<SqlException>()
                    .WaitAndRetryAsync(3,
                        retryAttempt => TimeSpan.FromMilliseconds(500));
        }
    }
}
