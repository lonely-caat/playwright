using AutoFixture;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Contract
{
    public class ConsumerIdentityInfoTests
    {
        private readonly Fixture _fixture;

        public ConsumerIdentityInfoTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void ShouldGet_FullText()
        {
            var expect = _fixture.Create<string>();
                    
            var actual = new ConsumerIdentityInfo
            {
                IdentityInfoType = IdentityInfoType.Card,
                DisplayValue = expect
            };

            Assert.Equal($"Card is {expect}", actual.FullText);
        }
    }
}
