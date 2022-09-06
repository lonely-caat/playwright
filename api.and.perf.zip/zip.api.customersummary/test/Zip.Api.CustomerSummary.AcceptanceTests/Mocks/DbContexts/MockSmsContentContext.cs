using System.Threading.Tasks;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
{
    public class MockSmsContentContext : ISmsContentContext
    {
        private readonly IFixture _fixture;

        public MockSmsContentContext()
        {
            _fixture = new Fixture();
        }

        public async Task<SmsContent> GetAsync(int id)
        {
            return await Task.FromResult(_fixture.Create<SmsContent>());
        }

        public async Task<SmsContent> GetAsync(string name)
        {
            return await Task.FromResult(_fixture.Create<SmsContent>());
        }
    }
}
