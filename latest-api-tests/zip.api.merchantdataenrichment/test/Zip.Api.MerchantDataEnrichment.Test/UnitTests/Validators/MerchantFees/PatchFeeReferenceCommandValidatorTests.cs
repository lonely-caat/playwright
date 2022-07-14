using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.MerchantFees.Command.PatchFeeReference;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.MerchantFees
{
    public class PatchFeeReferenceCommandValidatorTests : CommonTestsFixture
    {
        private readonly PatchFeeReferenceCommandValidator _validator;

        public PatchFeeReferenceCommandValidatorTests()
        {
            _validator = new PatchFeeReferenceCommandValidator();
        }

        [Fact]
        public void Given_Valid_Data_Validator_Should_Have_No_Error()
        {
            // Arrange
            var request = Fixture.Create<PatchFeeReferenceCommand>();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_Invalid_Data_Validator_Should_Have_Error()
        {
            // Arrange
            var request = Fixture.Build<PatchFeeReferenceCommand>()
                                 .Without(x => x.WebhookId)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeFalse();
        }
    }
}