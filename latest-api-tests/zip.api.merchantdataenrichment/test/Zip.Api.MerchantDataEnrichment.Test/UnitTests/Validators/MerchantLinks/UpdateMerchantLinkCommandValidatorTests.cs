using System;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.MerchantLinks.Command.UpdateMerchantLink;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.MerchantLinks
{
    public class UpdateMerchantLinkCommandValidatorTests : CommonTestsFixture
    {
        private readonly UpdateMerchantLinkCommandValidator _validator;

        public UpdateMerchantLinkCommandValidatorTests()
        {
            _validator = new UpdateMerchantLinkCommandValidator();
        }

        [Fact]
        public void Given_Valid_Data_Validator_Should_Have_No_Error()
        {
            // Arrange
            var request = Fixture.Build<UpdateMerchantLinkCommand>()
                                 .Without(x => x.MerchantDetailId)
                                 .Without(x => x.ZipBranchId)
                                 .Without(x => x.ZipCompanyId)
                                 .With(x => x.Active, false)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_Invalid_Data_Validator_Should_Have_Error()
        {
            // Arrange
            var request = Fixture.Build<UpdateMerchantLinkCommand>()
                                 .Without(x => x.MerchantLinkId)
                                 .With(x => x.MerchantDetailId, Guid.Empty)
                                 .With(x => x.ZipMerchantId, -1)
                                 .With(x => x.ZipBranchId, -1)
                                 .With(x => x.ZipCompanyId, -1)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeFalse();
        }
    }
}