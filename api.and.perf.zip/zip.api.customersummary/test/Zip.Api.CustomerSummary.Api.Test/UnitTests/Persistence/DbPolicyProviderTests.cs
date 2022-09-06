using Xunit;
using Zip.Api.CustomerSummary.Persistence;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence
{
    public class DbPolicyProviderTests
    {
        [Fact]
        public void Should_GetRetryPolicy()
        {
            Assert.NotNull(new DbPolicyProvider().GetRetryPolicy());
        }
    }
}
