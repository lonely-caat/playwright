using System.Data;

namespace Zip.Api.CustomerSummary.Persistence.Interfaces
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetDbConnection();
    }
}
