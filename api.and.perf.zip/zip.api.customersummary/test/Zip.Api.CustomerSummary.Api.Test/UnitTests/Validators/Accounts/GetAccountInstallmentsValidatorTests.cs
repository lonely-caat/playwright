using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInstallments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Accounts
{
    public class GetAccountInstallmentsValidatorTests
    {
        private readonly GetAccountInstallmentsValidator _validator;

        public GetAccountInstallmentsValidatorTests()
        {
            _validator = new GetAccountInstallmentsValidator();
        }

        [Fact]
        public void Given_AllGood_Should_Pass()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.AccountId, 1);
        }

        [Fact]
        public void Given_InvalidValue_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, 0);
        }
    }
}
