using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendAccountClosedEmail;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Communications
{
    public class SendAccountClosedEmailCommandValidatorTest
    {
        private readonly SendAccountClosedEmailCommandValidator _validator;

        public SendAccountClosedEmailCommandValidatorTest()
        {
            _validator = new SendAccountClosedEmailCommandValidator();
        }

        [Fact]
        public void Given_EmailEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.Email, string.Empty);
        }

        [Fact]
        public void Given_FirstNameEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.FirstName, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.FirstName, string.Empty);
        }

        [Fact]
        public void Given_ProductNull_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Product, null as string);
        }

        [Fact]
        public void Given_AccountNumberNull_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, null as string);
        }

        [Fact]
        public void Should_pass()
        {
            var result = _validator.Validate(new SendAccountClosedEmailCommand()
            {
                Product = ProductClassification.zipMoney.ToString(),
                Email = "johnny.vuong@zip.co",
                FirstName = "johnny",
                AccountNumber = "123456"
            });

            Assert.True(result.IsValid);
        }
    }
}
