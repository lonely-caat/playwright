using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Pagination;
using Zip.MerchantDataEnrichment.Application.MerchantDetails.Query.SearchMerchantDetails;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.MerchantDetails
{
    public class SearchMerchantDetailsQueryValidatorTests : CommonTestsFixture
    {
        private readonly SearchMerchantDetailsQueryValidator _validator;

        public SearchMerchantDetailsQueryValidatorTests()
        {
            _validator = new SearchMerchantDetailsQueryValidator();
        }

        [Fact]
        public void Given_Valid_Data_Validator_Should_Have_No_Error()
        {
            // Arrange
            var paginationSetting = new PaginationSetting { Page = 1 };
            var request = Fixture.Build<SearchMerchantDetailsQuery>()
                                 .With(x => x.MerchantName, "MERCHANT_NAME")
                                 .With(x => x.ABN, "12345678901")
                                 .With(x => x.ACN, "345678901")
                                 .With(x => x.Postcode, "0870")
                                 .With(x => x.SingleLineAddress, "SINGLE_LINE_ADDRESS")
                                 .With(x => x.PaginationSetting, paginationSetting)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("AB", "12345678901", "345678901", "0870", "SINGLE_LINE_ADDRESS", 20, 10)]
        [InlineData("MERCHANT_NAME", "1234567890", "345678901", "0870", "SINGLE_LINE_ADDRESS", 20, 10)]
        [InlineData("MERCHANT_NAME", "12345678901", "34567890", "0870", "SINGLE_LINE_ADDRESS", 20, 10)]
        [InlineData("MERCHANT_NAME", "12345678901", "456789012", "0870", "SINGLE_LINE_ADDRESS", 20, 10)]
        [InlineData("MERCHANT_NAME", "12345678901", "345678901", "123456", "SINGLE_LINE_ADDRESS", 20, 10)]
        [InlineData("MERCHANT_NAME", "12345678901", "345678901", "0870", "INVALID", 20, 10)]
        [InlineData("MERCHANT_NAME", "12345678901", "345678901", "0870", "SINGLE_LINE_ADDRESS", 101, 10)]
        [InlineData("MERCHANT_NAME", "12345678901", "345678901", "0870", "SINGLE_LINE_ADDRESS", 20, -1)]
        public void Given_InValid_Data_Validator_Should_Have_Error(
            string merchantName,
            string abn,
            string acn,
            string postcode,
            string singleLineAddress,
            int take,
            int skip)
        {
            // Arrange
            var paginationSetting = new PaginationSetting { PageSize = take, Page = skip };
            var request = Fixture.Build<SearchMerchantDetailsQuery>()
                                 .With(x => x.MerchantName, merchantName)
                                 .With(x => x.ABN, abn)
                                 .With(x => x.ACN, acn)
                                 .With(x => x.Postcode, postcode)
                                 .With(x => x.SingleLineAddress, singleLineAddress)
                                 .With(x => x.PaginationSetting, paginationSetting)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeFalse();
        }
    }
}