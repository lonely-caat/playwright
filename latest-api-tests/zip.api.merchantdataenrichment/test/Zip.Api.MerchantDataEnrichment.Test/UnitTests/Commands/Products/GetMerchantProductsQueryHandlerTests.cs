using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Pagination;
using Zip.MerchantDataEnrichment.Application.Products.Query.GetMerchantProducts;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.Products
{
    public class GetMerchantProductsQueryHandlerTests : CommonTestsFixture
    {
        private readonly GetMerchantProductsQueryHandler _target;

        public GetMerchantProductsQueryHandlerTests()
        {
            var logger = new Mock<ILogger<GetMerchantProductsQueryHandler>>();

            _target = new GetMerchantProductsQueryHandler(logger.Object,
                                                          Mapper,
                                                          DbContext);
        }

        [Fact]
        public async Task Given_Data_Handle_Should_Work_Correctly()
        {
            // Arrange
            const long ZIP_MERCHANT_ID = 1;
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .Without(x => x.VcnTransactions)
                                        .Without(x => x.MerchantCardAcceptorLocators)
                                        .Without(x => x.MerchantLinks)
                                        .Without(x => x.MerchantOpenLoopProducts)
                                        .Create();
            await DbContext.MerchantDetails.AddAsync(merchantDetail, CancellationToken);

            var product1 = Fixture.Build<OpenLoopProduct>()
                                  .With(x => x.Id, new Guid(Constants.OpenLoopProductId1))
                                  .Without(x => x.MerchantOpenLoopProducts)
                                  .Without(x => x.FeeReferences)
                                  .Create();
            var product2 = Fixture.Build<OpenLoopProduct>()
                                  .With(x => x.Id, new Guid(Constants.OpenLoopProductId2))
                                  .Without(x => x.MerchantOpenLoopProducts)
                                  .Without(x => x.FeeReferences)
                                  .Create();
            await DbContext.OpenLoopProducts.AddRangeAsync(product1, product2);

            var merchantOpenLoopProduct1 = Fixture.Build<MerchantOpenLoopProduct>()
                                                  .With(x => x.ZipMerchantId, ZIP_MERCHANT_ID)
                                                  .With(x => x.OpenLoopProductId, new Guid(Constants.OpenLoopProductId1))
                                                  .With(x => x.MerchantDetailId, merchantDetail.Id)
                                                  .Without(x => x.OpenLoopProduct)
                                                  .Without(x => x.MerchantDetail)
                                                  .Create();
            var merchantOpenLoopProduct2 = Fixture.Build<MerchantOpenLoopProduct>()
                                                  .With(x => x.ZipMerchantId, ZIP_MERCHANT_ID)
                                                  .With(x => x.OpenLoopProductId, new Guid(Constants.OpenLoopProductId2))
                                                  .With(x => x.MerchantDetailId, merchantDetail.Id)
                                                  .Without(x => x.OpenLoopProduct)
                                                  .Without(x => x.MerchantDetail)
                                                  .Create();
            await DbContext.MerchantOpenLoopProducts.AddRangeAsync(merchantOpenLoopProduct1,
                                                                   merchantOpenLoopProduct2);

            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<GetMerchantProductsQuery>()
                                 .With(x => x.ZipMerchantId, ZIP_MERCHANT_ID)
                                 .With(x => x.PaginationSetting, new PaginationSetting { PageSize = 1, Page = 1 })
                                 .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.MerchantOpenLoopProducts.Items.Count().Should().Be(1);
            actual.MerchantOpenLoopProducts.TotalPageCount.Should().Be(2);
            actual.MerchantOpenLoopProducts.TotalRowCount.Should().Be(2);
        }
    }
}