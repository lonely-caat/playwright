using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Models
{
    public class VerifyAddressRequestTest
    {
        [Fact]
        public void Given_Valid_UnitNumber_When_Instantiate_VerifyAddressRequest_Then_AddressLine_Should_Be_Correct()
        {
            // Arrange
            var fixture = new Fixture();
            var unitNumber = fixture.Create<string>();
            var streetNumber = fixture.Create<string>();
            var streetName = fixture.Create<string>();
            var locality = fixture.Create<string>();
            var postcode = fixture.Create<string>();
            var state = fixture.Create<string>();

            var expectedAddressLine = $"{unitNumber}/{streetNumber} {streetName}";
            
            // Action
            var actual = new VerifyAddressRequest(unitNumber, streetNumber, streetName, locality, postcode, state);
            
            // Assert
            actual.AddressLine1.Should().BeEquivalentTo(expectedAddressLine);
            actual.Locality.Should().BeEquivalentTo(locality);
            actual.Postcode.Should().BeEquivalentTo(postcode);
            actual.State.Should().BeEquivalentTo(state);
        }
        
        [Fact]
        public void Given_Invalid_UnitNumber_When_Instantiate_VerifyAddressRequest_Then_AddressLine_Should_Be_Correct()
        {
            // Arrange
            var fixture = new Fixture();
            var unitNumber = string.Empty;
            var streetNumber = fixture.Create<string>();
            var streetName = fixture.Create<string>();
            var locality = fixture.Create<string>();
            var postcode = fixture.Create<string>();
            var state = fixture.Create<string>();

            var expectedAddressLine = $"{streetNumber} {streetName}";

            // Action
            var actual = new VerifyAddressRequest(unitNumber, streetNumber, streetName, locality, postcode, state);

            // Assert
            actual.AddressLine1.Should().BeEquivalentTo(expectedAddressLine);
            actual.Locality.Should().BeEquivalentTo(locality);
            actual.Postcode.Should().BeEquivalentTo(postcode);
            actual.State.Should().BeEquivalentTo(state);
        }
    }
}
