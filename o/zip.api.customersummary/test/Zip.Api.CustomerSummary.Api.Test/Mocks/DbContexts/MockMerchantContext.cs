using System.Threading.Tasks;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockMerchantContext : IMerchantContext
    {
        private readonly IFixture _fixture = new Fixture();

        public Task<Merchant> GetAsync(long id)
        {
            return Task.FromResult<Merchant>(new Merchant
            {
                Id = _fixture.Create<long>(),
                Name = _fixture.Create<string>(),
                URL = _fixture.Create<string>()
            });
        }
    }
}
