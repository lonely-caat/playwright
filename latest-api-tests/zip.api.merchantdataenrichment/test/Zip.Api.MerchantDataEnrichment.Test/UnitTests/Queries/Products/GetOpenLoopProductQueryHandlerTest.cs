using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Models;
using Zip.MerchantDataEnrichment.Application.Common.Pagination;
using Zip.MerchantDataEnrichment.Application.Products.Query.GetOpenLoopProducts;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Queries.Products
{
    public class GetOpenLoopProductQueryHandlerTest : CommonTestsFixture
    {
        private readonly GetOpenLoopProductQueryHandler _target;
        public GetOpenLoopProductQueryHandlerTest()
        {
            var logger = new Mock<ILogger<GetOpenLoopProductQueryHandler>>();

            _target = new GetOpenLoopProductQueryHandler(logger.Object, DbContext,
                                                             Mapper);
        }

        [Fact]
        public async Task Given_Valid_Pagination_Request_It_Should_Return_OpenLoopProducts()
        {
            // Arrange
            var openLoopProducts = Fixture.CreateMany<OpenLoopProduct>(3);
            await DbContext.OpenLoopProducts.AddRangeAsync(openLoopProducts);
            await DbContext.SaveChangesAsync(CancellationToken);
            var models = Mapper.Map<List<OpenLoopProductItem>>(openLoopProducts);

            var pageSettings = new PaginationSetting
            {
                Page = 1,
                PageSize = 10
            };
            var query = Fixture.Build<GetOpenLoopProductsQuery>()
                               .With(p => p.PaginationSetting, pageSettings)
                               .Create();

            var result = await _target.Handle(query, CancellationToken);

            // Assert
            result.OpenLoopProducts.Should().NotBeNull();
            result.OpenLoopProducts.Items.Should().NotBeNull();

            result.OpenLoopProducts.Items.Should().HaveCount(3);
            result.OpenLoopProducts.Items.Should().BeEquivalentTo(models);
        }
    }
}
