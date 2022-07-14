using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.MerchantFees.Command.CreateFeeReference;
using Zip.MerchantDataEnrichment.Domain.Enums;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.MerchantFees
{
    public class CreateFeeReferenceCommandValidatorTests : CommonTestsFixture
    {
        private readonly CreateFeeReferenceCommandValidator _validator;

        public CreateFeeReferenceCommandValidatorTests()
        {
            _validator = new CreateFeeReferenceCommandValidator();
        }

        [Fact]
        public void Given_Valid_Data_Validator_Should_Have_No_Error()
        {
            // Arrange
            var request = Fixture.Build<CreateFeeReferenceCommand>()
                                 .With(x => x.TransactionType, TransactionType.Refund)
                                 .With(x => x.TransactionAmount, "0.21")
                                 .With(x => x.FeeType, FeeType.ServiceFee)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_Invalid_Data_Validator_Should_Have_Error()
        {
            // Arrange
            var request = Fixture.Build<CreateFeeReferenceCommand>()
                                 .Without(x => x.WebhookId)
                                 .Without(x => x.TransactionAmount)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeFalse();
        }
    }
}