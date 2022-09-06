using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Orders.Command;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Accounts
{
    public class CancelOrderInstallmentsCommandValidatorTests
    {
        private readonly CancelOrderInstallmentsCommandValidator _validator;

        public CancelOrderInstallmentsCommandValidatorTests()
        {
            _validator = new CancelOrderInstallmentsCommandValidator();
        }

        [Fact]
        public void Given_AllGood_Should_Pass()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.AccountId, 1);
            _validator.ShouldNotHaveValidationErrorFor(x => x.OrderId, 1);
        }

        [Fact]
        public void Given_InvalidValue_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.OrderId, 0);
        }
    }
}
