using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IMfaContext
    {
        Task<MfaCode> GetTokenAsync(MfaEntityType mfaEntityType, long entityId);
        string CreateToken(string secret, long iterationNumber, string destination, MfaType mfaType, int digits = 6);
        Task<MfaCode> SaveAsync(MfaCode code);
        Task<MfaCode> GetAsync(long id);

        Task<int> ExpireTokensAsync(MfaEntityType mfaEntityType, long entityId);
    }
}