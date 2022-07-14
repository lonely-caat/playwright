using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Factories;

namespace Zip.Api.MerchantDataEnrichment.Test.IntegrationTests
{
    public class HealthCheckTests : IClassFixture<ApiFactory<TestStartup>>
    {
        private readonly ApiFactory<TestStartup> _factory;

        public HealthCheckTests(ApiFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetHealthReturnHealthyStatus()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            using var response = await client.GetAsync(new Uri("/health", UriKind.Relative));
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();

            // Assert
            responseText.Should().Be("Healthy");
        }
    }
}