using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Accounts
{
    public class LockAccountCommandValidatorTest
    {
        private readonly LockAccountCommandValidator _validator;

        public LockAccountCommandValidatorTest()
        {
            _validator = new LockAccountCommandValidator();
        }

        [Fact]
        public void Given_InvalidConsumerId_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
        }

        [Fact]
        public void Given_InvalidAccountId_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, 0);
        }

        [Fact]
        public void Given_AllGood_Should_pass()
        {
            var result = _validator.Validate(new LockAccountCommand()
            {
                ConsumerId = 2485,
                AccountId = 1412,
                ChangedBy = "alvin.ho@zip.co",
                Reason = "Test lock account"
            });

            Assert.True(result.IsValid);
        }
    }
}
