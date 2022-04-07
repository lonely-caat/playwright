using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Command.AddAttributeAndLockAccount;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Accounts
{
    public class AddAttributeAndLockAccountValidatorTest
    {
        private readonly AddAttributeAndLockAccountValidator _validator;

        public AddAttributeAndLockAccountValidatorTest()
        {
            _validator = new AddAttributeAndLockAccountValidator();
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
        public void Given_InvalidAttribute_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Attribute, "");
        }

        [Fact]
        public void Given_InvalidReason_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Reason, "");
        }

        [Fact]
        public void Given_AllGood_Should_pass()
        {
            var result = _validator.Validate(new AddAttributeAndLockAccountCommand()
            {
                ConsumerId = 2485,
                AccountId = 1412,
                ChangedBy = "johnny.vuong@zip.co",
                Reason = "Test lock account",
                Attribute = "testAttribute"
            });

            Assert.True(result.IsValid);
        }
    }
}
