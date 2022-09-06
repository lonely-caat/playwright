using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateContact;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Consumers
{
    public class UpdateContactCommandValidatorTest
    {
        private readonly UpdateContactCommandValidator _validator;

        public UpdateContactCommandValidatorTest()
        {
            _validator = new UpdateContactCommandValidator();
        }

        [Fact]
        public void  Given_ConsumerIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, -1);
        }

        [Fact]
        public void Should_pass()
        {
            var r1 = _validator.Validate(new UpdateContactCommand()
            {
                ConsumerId = 291
            });

            Assert.False(r1.IsValid);

            var r2 = _validator.Validate(new UpdateContactCommand()
            {
                ConsumerId = 201,
                Email = "shan.ke@zip.co"
            });

            Assert.True(r2.IsValid);

            var r3 = _validator.Validate(new UpdateContactCommand()
            {
                ConsumerId = 291,
                Mobile = "42831921"
            });

            Assert.True(r3.IsValid);
        }
    }
}
