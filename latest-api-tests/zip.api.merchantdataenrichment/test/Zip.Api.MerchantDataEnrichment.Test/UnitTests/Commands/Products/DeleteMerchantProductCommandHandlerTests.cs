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
using Zip.MerchantDataEnrichment.Application.Products.Command.DeleteMerchantProduct;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.Products
{
    public class DeleteMerchantProductCommandHandlerTests : CommonTestsFixture
    {
        private readonly DeleteMerchantProductCommandHandler _target;

        public DeleteMerchantProductCommandHandlerTests()
        {
            var logger = new Mock<ILogger<DeleteMerchantProductCommandHandler>>();

            _target = new DeleteMerchantProductCommandHandler(logger.Object,
                                                              Mapper,
                                                              DbContext);
        }

        [Theory, AutoData]
        public async Task Given_No_MerchantOpenLoopProduct_Handle_Should_Throw(DeleteMerchantProductCommand request)
        {
            // Arrange & Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<CustomException>();
        }

        [Fact]
        public async Task Given_MerchantOpenLoopProduct_Exists_Handle_Should_Work_Correctly()
        {
            // Arrange
            var openLoopProduct = Fixture.Build<OpenLoopProduct>()
                                         .Without(x => x.MerchantOpenLoopProducts)
                                         .Without(x => x.FeeReferences)
                                         .Create();
            await DbContext.OpenLoopProducts.AddAsync(openLoopProduct, CancellationToken);

            var merchantOpenLoopProduct = Fixture.Build<MerchantOpenLoopProduct>()
                                                 .With(x => x.OpenLoopProductId, openLoopProduct.Id)
                                                 .Without(x => x.MerchantDetail)
                                                 .Create();
            await DbContext.MerchantOpenLoopProducts.AddAsync(merchantOpenLoopProduct, CancellationToken);
            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<DeleteMerchantProductCommand>()
                                 .With(x => x.MerchantOpenLoopProductId, merchantOpenLoopProduct.Id)
                                 .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.IsSuccess.Should().BeTrue();
            actual.Message.Should().NotBeNullOrEmpty();
        }
    }
}
