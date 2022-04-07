using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdatePhoneStatus;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Consumers
{
    public class UpdatePhoneStatusCommandValidatorTest
    {
        private readonly UpdatePhoneStatusCommandValidator _validator;
        public UpdatePhoneStatusCommandValidatorTest()
        {
            _validator = new UpdatePhoneStatusCommandValidator();
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, -1);
        }

        [Fact]
        public void Given_PhoneIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneId, -1);
        }

        [Fact]
        public void Given_PreferredInvalid_Should_have_error()
        {
            var result = _validator.Validate(new UpdatePhoneStatusCommand()
            {
                Deleted = null,
                Active = null,
                Preferred = true
            });

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_DeletedInvalid_Should_have_error()
        {
            var result = _validator.Validate(new UpdatePhoneStatusCommand()
            {
                Deleted = true,
                Active = null,
                Preferred = null
            });

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_ActiveInvalid_Should_have_error()
        {
            var result = _validator.Validate(new UpdatePhoneStatusCommand()
            {
                Deleted = null,
                Active = true,
                Preferred = null
            });

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass()
        {
            var result = _validator.Validate(new UpdatePhoneStatusCommand()
            {
                Deleted = true,
                Active = true,
                Preferred = true,
                ConsumerId = 1,
                PhoneId = 2
            });

            Assert.True(result.IsValid);
        }
    }
}
