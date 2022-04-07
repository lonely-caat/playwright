using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Accounts
{
    public class CloseAccountCommandValidatorTest
    {
        private readonly CloseAccountCommandValidator _validator;

        public CloseAccountCommandValidatorTest()
        {
            _validator = new CloseAccountCommandValidator();
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
        }

        [Fact]
        public void Given_AccountIdInvalid_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, -1);
        }

        [Fact]
        public void Given_CreditProfileIdInvalid_Should_HaveError()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CreditProfileId, -2);
        }

        [Fact]
        public void Given_CreditStateTypeNull_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CreditStateType, (CreditProfileStateType?)null);
        }

        [Fact]
        public void Given_AllGood_Should_pass()
        {
            var result = _validator.Validate(new CloseAccountCommand()
            {
                ConsumerId = 201,
                AccountId = 143,
                ChangedBy = "Shan Ke",
                CreditProfileId = 302,
                CreditStateType = CreditProfileStateType.ApplicationCompleted
            });

            Assert.True(result.IsValid);
        }
    }
}
