using System.Threading.Tasks;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockAccountTypeContext : IAccountTypeContext
    {
        private readonly Fixture _fixture;

        public MockAccountTypeContext()
        {
            _fixture = new Fixture();
        }

        public Task<AccountType> GetAsync(long id)
        {
            return Task.FromResult<AccountType>(_fixture.Create<AccountType>());
        }
    }
}
