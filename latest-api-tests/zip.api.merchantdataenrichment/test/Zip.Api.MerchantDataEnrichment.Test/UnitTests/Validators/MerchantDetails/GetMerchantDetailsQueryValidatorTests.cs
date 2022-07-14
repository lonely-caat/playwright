using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Query.GetMerchantDetails;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Query.GetMerchantDetails.Models;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.MerchantDetails
{
    public class GetMerchantDetailsQueryValidatorTests : CommonTestsFixture
    {
        private readonly GetMerchantDetailsQueryValidator _validator;

        public GetMerchantDetailsQueryValidatorTests()
        {
            _validator = new GetMerchantDetailsQueryValidator();
        }

        [Fact]
        public void Given_Valid_Data_Validator_Should_Have_No_Error()
        {
            // Arrange
            var query = Fixture.Create<GetMerchantDetailsQuery>();

            // Act & Assert
            _validator.Validate(query).IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_Empty_Data_Validator_Should_Have_Error()
        {
            // Arrange
            var query = new GetMerchantDetailsQuery { MerchantDetailLookupItems = new List<MerchantDetailLookupItem>() };

            // Act & Assert
            _validator.Validate(query).IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_Invalid_MerchantDetailLookupItems_Validator_Should_Have_Error()
        {
            // Arrange
            var query = new GetMerchantDetailsQuery
            {
                MerchantDetailLookupItems = Fixture.CreateMany<MerchantDetailLookupItem>(51).ToList()
            };

            // Act & Assert
            _validator.Validate(query).IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_Invalid_TransactionCorrelationGuid_Validator_Should_Have_Error()
        {
            // Arrange
            var query = new GetMerchantDetailsQuery
            {
                MerchantDetailLookupItems = Fixture.Build<MerchantDetailLookupItem>()
                                                   .Without(x => x.TransactionCorrelationGuid)
                                                   .CreateMany(3)
                                                   .ToList()
            };

            // Act & Assert
            _validator.Validate(query).IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_Invalid_CardAcceptorLocator_Validator_Should_Have_Error()
        {
            // Arrange
            var query = new GetMerchantDetailsQuery
            {
                MerchantDetailLookupItems = Fixture.Build<MerchantDetailLookupItem>()
                                                   .Without(x => x.CardAcceptorLocator)
                                                   .CreateMany(3)
                                                   .ToList()
            };

            // Act & Assert
            _validator.Validate(query).IsValid.Should().BeFalse();
        }
    }
}