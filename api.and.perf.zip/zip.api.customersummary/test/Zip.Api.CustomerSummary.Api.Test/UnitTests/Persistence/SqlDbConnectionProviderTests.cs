using System;
using Xunit;
using Zip.Api.CustomerSummary.Persistence;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence
{
    public class SqlDbConnectionProviderTests
    {
        [Fact]
        public void Given_NullConnString_ShouldThrow_Ex()
        {
            Assert.Throws<ArgumentNullException>(() => new SqlDbConnectionProvider(null));
            Assert.Throws<ArgumentNullException>(() => new SqlDbConnectionProvider(string.Empty));
        }

        [Fact]
        public void Should_expose_conn_string()
        {
            var expected = "test conn string";
            var dp = new SqlDbConnectionProvider(expected);

            Assert.Equal(expected, dp.ConnectionString);
        }
    }
}
