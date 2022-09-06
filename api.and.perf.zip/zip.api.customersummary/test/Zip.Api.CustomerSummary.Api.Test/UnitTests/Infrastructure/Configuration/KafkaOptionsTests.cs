using AutoFixture;
using Xunit;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Configuration
{
    public class KafkaOptionsTests
    {
        private readonly IFixture _fixture;

        public KafkaOptionsTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Should_equal()
        {
            var svr = _fixture.Create<string>();
            var url = _fixture.Create<string>();
            var id = _fixture.Create<string>();

            var result = new CustomerSummary.Infrastructure.Configuration.KafkaOptions()
            {
                BootstrapServers = svr,
                SchemaRegistryUrl = url,
                ConsumerGroupId = id
            };

            Assert.Equal(svr, result.BootstrapServers);
            Assert.Equal(url, result.SchemaRegistryUrl);
            Assert.Equal(id, result.ConsumerGroupId);
        }
    }
}
