using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.MerchantLinks.Command.CreateMerchantLink;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.MerchantLinks
{
    public class CreateMerchantLinkCommandValidatorTests : CommonTestsFixture
    {
        private readonly CreateMerchantLinkCommandValidator _validator;

        public CreateMerchantLinkCommandValidatorTests()
        {
            _validator = new CreateMerchantLinkCommandValidator();
        }

        [Fact]
        public void Given_Valid_Data_Validator_Should_Have_No_Error()
        {
            // Arrange
            var request = Fixture.Build<CreateMerchantLinkCommand>()
                                 .With(x => x.ZipMerchantId, 1)
                                 .Without(x => x.ZipBranchId)
                                 .Without(x => x.ZipCompanyId)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_Invalid_Data_Validator_Should_Have_Error()
        {
            // Arrange
            var request = Fixture.Build<CreateMerchantLinkCommand>()
                                 .Without(x => x.MerchantDetailId)
                                 .With(x => x.ZipMerchantId, -1)
                                 .With(x => x.ZipBranchId, -1)
                                 .With(x => x.ZipCompanyId, -1)
                                 .With(x => x.MerchantAccountHash, string.Empty)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeFalse();
        }
    }
}