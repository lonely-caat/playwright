using System;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.CreateRepayment;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Payments
{
    public class CreateRepaymentCommandValidatorTest
    {
        private readonly CreateRepaymentCommandValidator _validator;

        public CreateRepaymentCommandValidatorTest()
        {
            _validator = new CreateRepaymentCommandValidator();
        }

        [Fact]
        public void Given_AccountIdInvalid_Should_Have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, 0);
        }

        [Fact]
        public void Given_AmountLessThan1_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Amount, 0);
        }

        [Fact]
        public void Given_StartDateInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.StartDate, DateTime.Now);
        }

        [Fact]
        public void Given_FrequencyNull_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Frequency, (Frequency?)null);
        }

        [Fact]
        public void Given_AllGood_Should_Pass()
        {
            var result = _validator.Validate(new CreateRepaymentCommand()
            {
                AccountId = 392,
                Amount = 22.9m,
                StartDate = DateTime.Now.AddDays(7),
                ChangedBy = "Shan Ke",
                Frequency = Frequency.Fortnightly
            });

            Assert.True(result.IsValid);
        }
    }
}
