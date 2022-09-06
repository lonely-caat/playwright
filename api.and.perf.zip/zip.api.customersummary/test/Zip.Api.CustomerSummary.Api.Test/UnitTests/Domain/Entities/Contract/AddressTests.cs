using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Contract
{
    public class AddressTests
    {
        private readonly Fixture _fixture;

        public AddressTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void Given_UnitNumber_ComposeFullAddress_Should_Be_Correct()
        {
            var target = _fixture.Create<Address>();

            var expect = $"{target.UnitNumber} {target.StreetNumber} {target.StreetName} {target.Suburb} {target.State} {target.PostCode}";

            target.FullAddress.Should()
                   .Be(expect);
        }

        [Fact]
        public void Given_No_UnitNumber_ComposeFullAddress_Should_Be_Correct()
        {
            var target = _fixture.Build<Address>()
                   .With(x => x.UnitNumber, string.Empty)
                   .Create();

            var expect = $"{target.StreetNumber} {target.StreetName} {target.Suburb} {target.State} {target.PostCode}";

            target.FullAddress.Should()
                   .Be(expect);
        }

        [Fact]
        public void ToString_Should_Be_Correct()
        {
            var target = _fixture.Create<Address>();

            var expect = $"{target.UnitNumber} {target.StreetNumber} {target.StreetName}, {target.Suburb} {target.State} {target.PostCode}";

            target.ToString()
                   .Should()
                   .Be(expect);
        }
    }
}
