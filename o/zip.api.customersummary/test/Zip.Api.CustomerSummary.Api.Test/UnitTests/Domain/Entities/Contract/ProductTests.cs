using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Contract
{
    public class ProductTests
    {
        [Fact]
        public void Dummy_Properties_Test()
        {
            var target = new Fixture().Create<Product>();

            target.Should()
                   .Be(target);
        }
    }
}
