using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Pagination;
using Zip.MerchantDataEnrichment.Application.Products.Query.GetMerchantProducts;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.Products
{
    public class GetMerchantProductsQueryValidatorTests : CommonTestsFixture
    {
        private readonly GetMerchantProductsQueryValidator _validator;

        public GetMerchantProductsQueryValidatorTests()
        {
            _validator = new GetMerchantProductsQueryValidator();
        }

        [Fact]
        public void Should_Be_Valid()
        {
            // Arrange
            var request = Fixture.Build<GetMerchantProductsQuery>()
                                 .With(x => x.ZipMerchantId, 1)
                                 .With(x => x.PaginationSetting, new PaginationSetting())
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Be_Invalid()
        {
            // Arrange
            var request = Fixture.Build<GetMerchantProductsQuery>()
                                 .With(x => x.ZipMerchantId, -1)
                                 .Without(x => x.PaginationSetting)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeFalse();
        }
    }
}