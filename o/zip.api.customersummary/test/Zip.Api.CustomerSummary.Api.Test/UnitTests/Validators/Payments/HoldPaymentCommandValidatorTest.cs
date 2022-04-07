using System;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.HoldPayment;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Payments
{
    public class HoldPaymentCommandValidatorTest
    {
        private readonly HoldPaymentCommandValidator _validator;

        public HoldPaymentCommandValidatorTest()
        {
            _validator = new HoldPaymentCommandValidator();
        }

        [Fact]
        public void Given_AccountIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, -1);
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, 0);
        }

        [Fact]
        public void Given_PastHoldDate_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.HoldDate, DateTime.Now.AddMinutes(-1));
        }

        [Fact]
        public void Given_AllGood()
        {
            var result = _validator.Validate(new HoldPaymentCommand()
            {
                AccountId = 392,
                HoldDate = DateTime.Now.AddMinutes(10)
            });

            Assert.True(result.IsValid);
        }
    }
}
