using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Polly.Retry;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence
{
    [ExcludeFromCodeCoverage]
    public class SqlContext : IDbContext
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;
        private readonly AsyncRetryPolicy _retryPolicy;

        public SqlContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _dbConnectionProvider = new SqlDbConnectionProvider(connectionString);
            _retryPolicy = new DbPolicyProvider().GetRetryPolicy();
        }

        public IDbConnection GetDbConnection()
        {
            return _dbConnectionProvider.GetDbConnection();
        }

        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null) where TResult : class
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                using (var cnn = GetDbConnection())
                {
                    return await cnn.QueryAsync<TResult>(sql, param);
                }
            });
        }

        public async Task<TResult> QuerySingleOrDefaultAsync<TResult>(string sql, object param = null)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                using (var cnn = GetDbConnection())
                {
                    return await cnn.QuerySingleOrDefaultAsync<TResult>(sql, param);
                }
            });
        }

        public async Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null)
        {
            return await QueryFirstOrDefaultAsync<dynamic>(sql, param);
        }

        public async Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                using (var cnn = GetDbConnection())
                {
                    return await cnn.QueryFirstOrDefaultAsync<TResult>(sql, param);
                }
            });
        }

        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                using (var cnn = GetDbConnection())
                {
                    return await cnn.ExecuteAsync(sql, param);
                }
            });
        }

        public async Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                using (var cnn = GetDbConnection())
                {
                    return await cnn.ExecuteScalarAsync<TResult>(sql, param);
                }
            });
        }
    }
}
