using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Contract
{
    public class ConsumerTests
    {
        private readonly Fixture _fixture;

        public ConsumerTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Dummy_Properties_Test2()
        {
            var linedConsumer = _fixture.Build<Consumer>().With(x => x.LinkedConsumer, null as Consumer).Create();
            var target = _fixture.Build<Consumer>().With(x => x.LinkedConsumer, linedConsumer).Create();

            target.Should()
                   .Be(target);
        }
    }
}
