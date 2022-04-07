using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Kinesis;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities
{
    public class KinesisCustomerRecordTests
    {
        [Fact]
        public void Test_Model_Serialization()
        {
            // Arrange
            var fixture = new Fixture();
            var expected = fixture.Create<KinesisCustomerRecord>();
            var actualJsonString = JsonConvert.SerializeObject(
                    expected,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            
            // Action
            var actual = JsonConvert.DeserializeObject<KinesisCustomerRecord>(actualJsonString);
            
            // Assert
            actual.Type.Should().BeEquivalentTo(expected.Type);
            actual.Event.Should().BeEquivalentTo(expected.Event);
            actual.ConsumerId.Should().Be(expected.ConsumerId);
            actual.ConsumerAttributes.Should().BeEquivalentTo(expected.ConsumerAttributes);
            actual.Email.Should().BeEquivalentTo(expected.Email);
            actual.EmailOrigination.Should().Be(expected.EmailOrigination);
            actual.FirstName.Should().BeEquivalentTo(expected.FirstName);
            actual.LastName.Should().BeEquivalentTo(expected.LastName);
            actual.Phone.Should().BeEquivalentTo(expected.Phone);
            actual.Product.Should().BeEquivalentTo(expected.Product);
            actual.FacebookId.Should().BeEquivalentTo(expected.FacebookId);
            actual.PayPalId.Should().BeEquivalentTo(expected.PayPalId);
            actual.LinkedInId.Should().BeEquivalentTo(expected.LinkedInId);
            actual.VedaId.Should().BeEquivalentTo(expected.VedaId);
            actual.PublicConsumerId.Should().BeEquivalentTo(expected.PublicConsumerId);
            actual.TimeStamp.Should().Be(expected.TimeStamp);
            actual.BillingAddress.Should().BeEquivalentTo(expected.BillingAddress);
            actual.ShippingAddress.Should().BeEquivalentTo(expected.ShippingAddress);
            actual.IdentificationDocument.Should().BeEquivalentTo(expected.IdentificationDocument);
            actual.GoogleAnalyticsDeviceIds.Should().BeEquivalentTo(expected.GoogleAnalyticsDeviceIds);
            actual.CardId.Should().BeEquivalentTo(expected.CardId);
            actual.CardNumber.Should().BeEquivalentTo(expected.CardNumber);
            actual.CardExpiry.Should().BeEquivalentTo(expected.CardExpiry);
            actual.BankId.Should().BeEquivalentTo(expected.BankId);
            actual.BankAccountNumber.Should().BeEquivalentTo(expected.BankAccountNumber);
            actual.BankBsb.Should().BeEquivalentTo(expected.BankBsb);
            actual.CreditProfileStateType.Should().BeEquivalentTo(expected.CreditProfileStateType);
            actual.AccountStatus.Should().BeEquivalentTo(expected.AccountStatus);
        }
    }
}
