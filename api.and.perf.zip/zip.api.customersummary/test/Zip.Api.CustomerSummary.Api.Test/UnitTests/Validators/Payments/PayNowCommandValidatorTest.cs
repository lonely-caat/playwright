using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.PayNow;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Payments
{
    public class PayNowCommandValidatorTest
    {
        private readonly PayNowCommandValidator _validator;

        public PayNowCommandValidatorTest()
        {
            _validator = new PayNowCommandValidator();
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
        }

        [Fact]
        public void Given_AmountInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Amount, 0);
        }

        [Fact]
        public void Given_OriginatorEmailEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.OriginatorEmail, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.OriginatorEmail, string.Empty);
        }

        [Fact]
        public void Given_OriginatorIpAddressUndetected_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.OriginatorIpAddress, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.OriginatorIpAddress, string.Empty);
        }

        [Fact]
        public void Given_Allgood()
        {
            var result = _validator.Validate(new PayNowCommand
            {
                ConsumerId = 20,
                Amount = 291m,
                OriginatorEmail = "shan.ke@zip.co",
                OriginatorIpAddress = "127.0.0.1"
            });

            Assert.True(result.IsValid);
        }
    }
}
