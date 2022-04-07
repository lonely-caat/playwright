using System;
using Xunit;
using Zip.Api.CustomerSummary.Api.Http;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure
{
    public class AuthenticatedHttpClientHandlerTest
    {
        [Fact]
        public void Given_NullToken_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AuthenticatedHttpClientHandler(null));
        }
    }
}
