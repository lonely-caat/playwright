using System;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.PayOrder;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Payments
{
    public class PayOrderCommandValidatorTests
    {
        private readonly PayOrderCommandValidator _validator;

        public PayOrderCommandValidatorTests()
        {
            _validator = new PayOrderCommandValidator();
        }
        
        [Fact]
        public void Given_Valid_Parameters_Should_Be_Ok()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.AccountId, 123);
            _validator.ShouldNotHaveValidationErrorFor(x => x.CustomerId, Guid.NewGuid());
            _validator.ShouldNotHaveValidationErrorFor(x => x.OrderId, 1);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Amount, 1);
        }

        [Fact]
        public void Given_Invalid_Parameters_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.CustomerId, Guid.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.OrderId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.Amount, 0);
        }
    }
}
