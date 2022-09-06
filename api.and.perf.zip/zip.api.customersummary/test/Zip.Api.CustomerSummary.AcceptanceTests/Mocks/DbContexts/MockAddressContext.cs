using AutoFixture;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
{
    public class MockAddressContext : IAddressContext
    {
        private readonly IFixture _fixture = new Fixture();

        public Task<Address> GetAsync(long consumerId)
        {
            var country = _fixture.Create<Country>();

            return Task.FromResult<Address>(new Address
            {
                Country = country,
                CountryCode = country.Id,
                PostCode = _fixture.Create<string>(),
                State = _fixture.Create<string>(),
                StreetName = _fixture.Create<string>(),
                StreetNumber = _fixture.Create<string>(),
                Suburb = _fixture.Create<string>(),
                UnitNumber = _fixture.Create<string>()
            });
        }

        public Task UpdateAsync(long consumerId, Address address)
        {
            return Task.FromResult(0);
        }
    }
}
