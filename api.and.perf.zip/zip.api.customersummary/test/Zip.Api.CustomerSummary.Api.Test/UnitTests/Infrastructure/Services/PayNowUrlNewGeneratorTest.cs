using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Models;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.PayNow;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.PayNow.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class PayNowUrlNewGeneratorTest : CommonTestsFixture
    {
        private readonly Mock<IPayNowLinkServiceProxy> _payNowLinkServiceProxy;

        private readonly PayNowUrlNewGenerator _target;

        public PayNowUrlNewGeneratorTest()
        {
            _payNowLinkServiceProxy = new Mock<IPayNowLinkServiceProxy>();
            _target = new PayNowUrlNewGenerator(_payNowLinkServiceProxy.Object);
        }

        [Fact]
        public async Task Test_GeneratePayNowUrlResponse()
        {
            // Arrange
            var request = Fixture.Create<GeneratePayNowUrlRequest>();
            var response = Fixture.Create<PayNowLinkResponse>();
            var expect = new GeneratePayNowUrlResponse
            {
                PayNowUrl = response.PayNowUrl,
                Reference = response.Reference
            };

            _payNowLinkServiceProxy.Setup(x => x.PayNowLinkAsync(
                                              It.Is<PayNowLinkRequest>(
                                                  y => y.Amount == request.Amount &&
                                                       y.ConsumerId == request.ConsumerId)))
                                   .ReturnsAsync(response);
            
            // Act
            var actual = await _target.GeneratePayNowUrlAsync(request);
            
            // Assert
            actual.Should().BeEquivalentTo(expect);
        }
    }
}
