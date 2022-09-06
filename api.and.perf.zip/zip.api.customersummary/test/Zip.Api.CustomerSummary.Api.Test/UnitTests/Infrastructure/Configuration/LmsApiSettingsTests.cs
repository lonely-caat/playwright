using System;
using AutoFixture;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Configuration
{
    public class LmsApiSettingsTests
    {
        private readonly IFixture _fixture;

        public LmsApiSettingsTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Should_equal()
        {
            var baseUrl = _fixture.Create<Uri>();
            var endpoint = _fixture.Create<string>();

            var result = new LmsApiSettings()
            {
                BaseUrl = baseUrl,
                GetAccountEndpoint = endpoint
            };

            Assert.Equal(baseUrl, result.BaseUrl);
            Assert.Equal(endpoint, result.GetAccountEndpoint);
        }
    }
}
