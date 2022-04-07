using System;
using System.Data;

namespace Zip.Api.CustomerSummary.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction GetTransaction();

        void Commit();

        void Rollback();
    }
}
