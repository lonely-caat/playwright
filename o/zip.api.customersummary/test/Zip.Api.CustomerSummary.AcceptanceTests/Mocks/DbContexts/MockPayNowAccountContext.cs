using AutoFixture;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Domain.Entities.PayNowUrlGenerator;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
{
    public class MockPayNowAccountContext : IPayNowAccountContext
    {
        private readonly IFixture _fixture;

        public MockPayNowAccountContext()
        {
            _fixture = new Fixture();
        }

        public Task<PayNowLinkAccount> GetPayNowLinkAccountAsync(long accountId)
        {
            return Task.FromResult(new PayNowLinkAccount
            {
                AccountId = _fixture.Create<long>(),
                Classification = _fixture.Create<ProductClassification>(),
                CountryId = _fixture.Create<CountryCode>()
            });
        }
    }
}
