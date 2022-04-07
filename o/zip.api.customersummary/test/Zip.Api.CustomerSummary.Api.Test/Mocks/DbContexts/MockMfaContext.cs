using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockMfaContext : IMfaContext
    {
        public Task<MfaCode> GetTokenAsync(MfaEntityType mfaEntityType, long entityId)
        {
            var result = entityId == int.MaxValue
                ? null
                : new MfaCode() { MfaEntityType = mfaEntityType, EntityId = entityId };
            return Task.FromResult(result);
        }

        public string CreateToken(string secret, long iterationNumber, string destination, MfaType mfaType, int digits = 6)
        {
            return "token";
        }

        public Task<MfaCode> SaveAsync(MfaCode code)
        {
            code.Id = 1;
            return Task.FromResult(code);
        }

        public Task<MfaCode> GetAsync(long id)
        {
            var result = id == int.MaxValue
                ? null
                : new MfaCode() { MfaEntityType = MfaEntityType.Merchant, Id = id };
            return Task.FromResult(result);
        }

        public Task<int> ExpireTokensAsync(MfaEntityType mfaEntityType, long entityId)
        {
            return Task.FromResult(1);
        }
    }
}