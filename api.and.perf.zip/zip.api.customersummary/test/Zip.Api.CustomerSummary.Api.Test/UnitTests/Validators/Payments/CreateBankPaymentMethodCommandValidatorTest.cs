using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.CreateBankPaymentMethod;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Payments
{
    public class CreateBankPaymentMethodCommandValidatorTest
    {
        private readonly CreateBankPaymentMethodCommandValidator _validator;

        public CreateBankPaymentMethodCommandValidatorTest()
        {
            _validator = new CreateBankPaymentMethodCommandValidator();
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_Have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
        }

        [Fact]
        public void Given_BSBNullOrEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.BSB, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.BSB, string.Empty);
        }

        [Fact]
        public void Given_AccountNameNullOrEmpty_Should_Have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountName, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.AccountName, string.Empty);
        }

        [Fact]
        public void Given_AccountNumberNullOrEmpty_Should_have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, string.Empty);
        }

        [Fact]
        public void Given_AllGood_Should_pass()
        {
            var result = _validator.Validate(new CreateBankPaymentMethodCommand()
            {
                AccountName = "Shan Ke",
                AccountNumber = "39829182",
                BSB = "028192",
                ConsumerId = 221,
            });

            Assert.True(result.IsValid);
        }
    }
}
