using System;
using System.Data;
using System.Data.SqlClient;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence
{
    public class SqlDbConnectionProvider : IDbConnectionProvider
    {
        public string ConnectionString { get; }

        public SqlDbConnectionProvider(string connectionString)
        {
            ConnectionString = string.IsNullOrEmpty(connectionString) ? throw new ArgumentNullException(nameof(connectionString)) : connectionString;
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
