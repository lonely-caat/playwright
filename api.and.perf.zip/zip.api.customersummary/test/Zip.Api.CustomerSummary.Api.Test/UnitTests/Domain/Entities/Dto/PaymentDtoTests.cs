using AutoFixture;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using ZipMoney.Services.Payments.Contract.Types;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Dto
{
    public class PaymentDtoTests
    {
        private readonly IFixture _fixture;

        public PaymentDtoTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Should_equal()
        {
            var countryCode = _fixture.Create<CountryCode>();
            var gateway = _fixture.Create<CardGateway>();
            var methodType = _fixture.Create<PaymentMethodType>();
            var type = _fixture.Create<PaymentType>();
            var paymentstatus = _fixture.Create<PaymentState>();

            var result = new PaymentDto()
            {
                CountryCode = countryCode,
                Gateway = gateway,
                MethodType = methodType,
                Type = type,
                Status = paymentstatus
            };

            Assert.Equal(countryCode.ToString(), result.CountryCodeString);
            Assert.Equal(gateway.ToString(), result.GatewayString);
            Assert.Equal(methodType.ToString(), result.MethodTypeString);
            Assert.Equal(type.ToString(), result.TypeString);
            Assert.Equal(paymentstatus.ToString(), result.StatusString);
        }
    }
}
