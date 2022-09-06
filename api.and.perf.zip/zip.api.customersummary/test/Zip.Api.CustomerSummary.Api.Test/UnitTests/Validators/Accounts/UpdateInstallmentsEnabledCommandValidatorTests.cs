using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Command.UpdateInstallmentsEnabled;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Accounts
{
    public class UpdateInstallmentsEnabledCommandValidatorTests
    {
        private readonly UpdateInstallmentsEnabledCommandValidator _validator;

        public UpdateInstallmentsEnabledCommandValidatorTests()
        {
            _validator = new UpdateInstallmentsEnabledCommandValidator();
        }

        [Fact]
        public void Given_AllGood_Should_Pass()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.AccountId, 1);
            _validator.ShouldNotHaveValidationErrorFor(x => x.AccountTypeId, 1);
            _validator.ShouldNotHaveValidationErrorFor(x => x.IsInstallmentsEnabled, true);
            _validator.ShouldNotHaveValidationErrorFor(x => x.IsInstallmentsEnabled, false);
        }

        [Fact]
        public void Given_InvalidValue_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.AccountTypeId, 0);
        }
    }
}