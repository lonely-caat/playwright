using System;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Command.SetDefaultPaymentMethod;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Payments
{
    public class SetDefaultPaymentMethodCommandValidatorTest
    {
        private readonly SetDefaultPaymentMethodCommandValidator _validator;

        public SetDefaultPaymentMethodCommandValidatorTest()
        {
            _validator = new SetDefaultPaymentMethodCommandValidator();
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, -1);
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
        }

        [Fact]
        public void Given_PaymentMethodIdEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.PaymentMethodId, Guid.Empty);
        }

        [Fact]
        public void Should_pass()
        {
            var result = _validator.Validate(new SetDefaultPaymentMethodCommand()
            {
                ConsumerId = 2392,
                PaymentMethodId = Guid.NewGuid()
            });

            Assert.True(result.IsValid);
        }
    }
}
