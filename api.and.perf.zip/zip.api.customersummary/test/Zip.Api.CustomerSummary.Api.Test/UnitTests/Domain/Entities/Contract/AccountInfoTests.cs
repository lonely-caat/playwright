using AutoFixture;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Contract
{
    public class AccountInfoTests
    {
        private readonly Fixture _fixture;
        
        public AccountInfoTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void ShouldGet_AccountHash()
        {
            var id = _fixture.Create<long>();

            var accountInfo = new AccountInfo
            {
                AccountId = id
            };

            Assert.Equal(id.ToString(), accountInfo.AccountHash);
        }
    }
}
