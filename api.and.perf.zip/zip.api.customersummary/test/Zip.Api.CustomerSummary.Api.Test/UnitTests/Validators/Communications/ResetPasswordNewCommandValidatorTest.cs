using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendResetPasswordEmailNew;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Communications
{
    public class ResetPasswordNewCommandValidatorTest
    {
        private readonly SendResetPasswordEmailNewCommandValidator _validator;

        public ResetPasswordNewCommandValidatorTest()
        {
            _validator = new SendResetPasswordEmailNewCommandValidator();
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
        public void Given_ClassificationNull_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Classification, (ProductClassification?)null);
        }

        [Fact]
        public void Should_pass()
        {
            var result = _validator.Validate(new SendResetPasswordEmailNewCommand()
            {
                Classification = ProductClassification.zipMoney,
                Email = "shan.ke@zip.co",
                FirstName = "shan"
            });

            Assert.True(result.IsValid);
        }

    }
}
