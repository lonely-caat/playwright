using System;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Extensions
{
    public class ProductClassificationExtensionsTest
    {
        [Theory]
        [InlineData(ProductClassification.zipMoney, "Zip Money")]
        [InlineData(ProductClassification.zipPay, "Zip Pay")]
        [InlineData(ProductClassification.zipBiz, "Zip Biz")]
        public void Given_Valid_ProductClassification_Should_Get_DisplayName_Correctly(
                ProductClassification classification,
                string expect)
        {   
            var actual = classification.DisplayName();

            actual.Should().BeEquivalentTo(expect);
        }
        
        [Fact]
        public void Given_Invalid_ProductClassification_Should_Throw_InvalidProductCodeException()
        {
            const ProductClassification actual = (ProductClassification)int.MaxValue;

            Action action = () => { actual.DisplayName(); };

            action.Should()
                   .Throw<InvalidProductCodeException>();
        }
    }
}
