using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendPayNowLink;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Communications
{
    public class SendPayNowLinkCommandValidatorTest
    {
        private readonly SendPayNowLinkCommandValidator _validator;

        public SendPayNowLinkCommandValidatorTest()
        {
            _validator = new SendPayNowLinkCommandValidator();
        }

        [Fact]
        public void Given_AmountInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Amount, -1);
            _validator.ShouldHaveValidationErrorFor(x => x.Amount, 0);
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, -1);
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
        }

        [Fact]
        public void Should_pass()
        {
            var result = _validator.Validate(new SendPayNowLinkCommand()
            {
                Amount = 203m,
                ConsumerId = 293
            });

            Assert.True(result.IsValid);
        }
    }
}
