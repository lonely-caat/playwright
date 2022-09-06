using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.UnitTests.Common;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateAddress;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Consumers
{
    public class UpdateAddressCommandValidatorTest : CommonTestFixture
    {
        private readonly UpdateAddressCommandValidator _validator;
        public UpdateAddressCommandValidatorTest()
        {
            _validator = new UpdateAddressCommandValidator();
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, -1);
        }

        [Fact]
        public void Should_pass()
        {
            var mockAddress = _fixture.Create<Address>(); 
            var result = _validator.Validate(new UpdateAddressCommand()
            {
                ConsumerId = 29382,
                Address = mockAddress
            });

            Assert.True(result.IsValid);
        }
    }
}
