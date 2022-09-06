using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Persistence.Interfaces
{
    public interface IDbContext : IDbConnectionProvider
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null) where T : class;

        Task<TResult> QuerySingleOrDefaultAsync<TResult>(string sql, object param = null);

        Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null);

        Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null);

        Task<int> ExecuteAsync(string sql, object param = null);

        Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null);
    }
}