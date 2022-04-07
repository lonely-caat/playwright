using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence
{
    [ExcludeFromCodeCoverage]
    public class SqlUnitOfWork : IUnitOfWork
    {
        protected IDbConnection _connection;
        protected IDbTransaction _transaction;
        protected bool disposed = false;

        protected readonly IDbConnectionProvider _dbConnectionProvider;

        public SqlUnitOfWork(string connectionString)
        {
            _dbConnectionProvider = new SqlDbConnectionProvider(connectionString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }

                if (_connection != null)
                {
                    _connection.Close();

                    _connection.Dispose();
                }
            }

            disposed = true;
        }

        public void Rollback()
        {
            this._transaction.Rollback();
        }

        public void Commit()
        {
            this._transaction.Commit();
        }

        public IDbTransaction GetTransaction()
        {
            _connection = _dbConnectionProvider.GetDbConnection();
            _connection.Open();

            return _connection.BeginTransaction();
        }
    }
}
