using AutoFixture;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
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
