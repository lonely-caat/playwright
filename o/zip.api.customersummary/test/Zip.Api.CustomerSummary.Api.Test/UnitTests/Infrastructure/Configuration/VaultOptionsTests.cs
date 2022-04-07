using AutoFixture;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Configuration
{
    public class VaultOptionsTests
    {
        private readonly IFixture _fixture;

        public VaultOptionsTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Should_equal()
        {
            var secretPath = _fixture.Create<string>();
            var tokenPath = _fixture.Create<string>();
            var url = _fixture.Create<string>();

            var result = new VaultOptions()
            {
                SecretPath = secretPath,
                TokenPath = tokenPath,
                Url = url

            };

            Assert.Equal(secretPath, result.SecretPath);
            Assert.Equal(tokenPath, result.TokenPath);
            Assert.Equal(url, result.Url);
        }
    }
}
