using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Factories;

namespace Zip.Api.CustomerSummary.Api.Test.IntegrationTests
{
    public class HealthCheckTests : IClassFixture<ApiFactory>
    {
        private readonly ApiFactory _factory;

        public HealthCheckTests(ApiFactory factory)
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
            responseText.Should().Be("{\"status\":\"Healthy\",\"details\":[]}");
        }
    }
}