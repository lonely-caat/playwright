using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Exceptions;
using Zip.MerchantDataEnrichment.Application.Products.Command.CreateMerchantProduct;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.Products
{
    public class CreateMerchantProductCommandHandlerTests : CommonTestsFixture
    {
        private readonly CreateMerchantProductCommandHandler _target;

        public CreateMerchantProductCommandHandlerTests()
        {
            var logger = new Mock<ILogger<CreateMerchantProductCommandHandler>>();

            _target = new CreateMerchantProductCommandHandler(logger.Object,
                                                              Mapper,
                                                              DbContext);
        }

        [Theory, AutoData]
        public async Task Given_No_Product_Handle_Should_Throw(CreateMerchantProductCommand request)
        {
            // Arrange & Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<CustomException>();
        }

        [Fact]
        public async Task Given_Product_Is_InActive_Handle_Should_Throw()
        {
            // Arrange
            var product = Fixture.Build<OpenLoopProduct>()
                                 .With(x => x.IsActive, false)
                                 .Without(x => x.MerchantOpenLoopProducts)
                                 .Without(x => x.FeeReferences)
                                 .Create();
            DbContext.OpenLoopProducts.Add(product);
            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<CreateMerchantProductCommand>()
                                 .With(x => x.ZipMerchantId, 1)
                                 .With(x => x.OpenLoopProductId, product.Id)
                                 .Create();

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<CustomException>();
        }

        [Fact]
        public async Task Given_Product_When_MerchantLink_Already_Exists_Handle_Should_Throw()
        {
            // Arrange
            const long ZIP_MERCHANT_ID = 1;
            var product = Fixture.Build<OpenLoopProduct>()
                                 .With(x => x.IsActive, true)
                                 .Without(x => x.MerchantOpenLoopProducts)
                                 .Without(x => x.FeeReferences)
                                 .Create();
            await DbContext.OpenLoopProducts.AddAsync(product, CancellationToken);

            var merchantLink = Fixture.Build<MerchantLink>()
                                      .With(x => x.ZipMerchantId, ZIP_MERCHANT_ID)
                                      .Without(x => x.MerchantDetail)
                                      .Create();
            await DbContext.MerchantLinks.AddAsync(merchantLink, CancellationToken);

            var merchantProduct = Fixture.Build<MerchantOpenLoopProduct>()
                                         .With(x => x.ZipMerchantId, ZIP_MERCHANT_ID)
                                         .With(x => x.MerchantDetailId, merchantLink.MerchantDetailId)
                                         .With(x => x.OpenLoopProductId, product.Id)
                                         .Without(x => x.OpenLoopProduct)
                                         .Without(x => x.MerchantDetail)
                                         .Create();
            await DbContext.MerchantOpenLoopProducts.AddAsync(merchantProduct, CancellationToken);
            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<CreateMerchantProductCommand>()
                                 .With(x => x.ZipMerchantId, ZIP_MERCHANT_ID)
                                 .With(x => x.OpenLoopProductId, product.Id)
                                 .Create();

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<CustomException>();
        }

        [Fact]
        public async Task Given_Product_And_MerchantLink_When_MerchantOpenLoopProduct_Not_Exists_Handle_Should_Work_Correctly()
        {
            // Arrange
            const long ZIP_MERCHANT_ID = 1;
            var product = Fixture.Build<OpenLoopProduct>()
                                 .With(x => x.IsActive, true)
                                 .Without(x => x.MerchantOpenLoopProducts)
                                 .Without(x => x.FeeReferences)
                                 .Create();
            await DbContext.OpenLoopProducts.AddAsync(product, CancellationToken);

            var merchantLink = Fixture.Build<MerchantLink>()
                                      .With(x => x.ZipMerchantId, ZIP_MERCHANT_ID)
                                      .Without(x => x.MerchantDetail)
                                      .Create();
            await DbContext.MerchantLinks.AddAsync(merchantLink, CancellationToken);
            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<CreateMerchantProductCommand>()
                                 .With(x => x.ZipMerchantId, ZIP_MERCHANT_ID)
                                 .With(x => x.OpenLoopProductId, product.Id)
                                 .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.MerchantOpenLoopProduct.ZipMerchantId.Should().Be(ZIP_MERCHANT_ID);
            actual.MerchantOpenLoopProduct.MerchantDetailId.Should().Be(merchantLink.MerchantDetailId);
            actual.MerchantOpenLoopProduct.OpenLoopProductId.Should().Be(product.Id);
            actual.MerchantOpenLoopProduct.OpenLoopProduct
                  .Should()
                  .BeEquivalentTo(product,
                                  opt => opt.Excluding(x => x.MerchantOpenLoopProducts)
                                            .Excluding(x => x.FeeReferences));
        }
    }
}