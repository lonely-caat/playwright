using AutoFixture;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using ZipMoney.Services.Payments.Contract.Types;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Dto
{
    public class PaymentMethodDtoTests
    {
        private readonly IFixture _fixture;

        public PaymentMethodDtoTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Should_equal()
        {
            var state = _fixture.Create<PaymentMethodState>();
            var countryCode = _fixture.Create<CountryCode>();
            var type = _fixture.Create<PaymentMethodType>();

            var result = new PaymentMethodDto()
            {
                State = state,
                CountryCode = countryCode,
                Type = type
            };

            Assert.Equal(state.ToString(), result.StateString);
            Assert.Equal(countryCode.ToString(), result.CountryCodeString);
            Assert.Equal(type.ToString(), result.TypeString);
        }
    }
}
