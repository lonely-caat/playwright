using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Products.Command.UpdateOpenLoopProduct;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.Products
{
    public class UpdateOpenLoopProductCommandHandlerTests : CommonTestsFixture
    {
        private readonly UpdateOpenLoopProductCommandHandler _target;

        public UpdateOpenLoopProductCommandHandlerTests()
        {
            var logger = new Mock<ILogger<UpdateOpenLoopProductCommandHandler>>();

            _target = new UpdateOpenLoopProductCommandHandler(logger.Object,
                                                              Mapper,
                                                              DbContext);
        }

        [Fact]
        public async Task Given_Valid_OpenLoopProduct_Should_be_Deactivated()
        {
            // Arrange
            var openLoopProduct1 = Fixture.Build<OpenLoopProduct>()
                                          .With(p => p.Id, new Guid(Constants.OpenLoopProductId1))
                                          .With(p => p.UpdatedDateTime, DateTime.Now.AddDays(-10))
                                          .Create();

            var entity = (await DbContext.OpenLoopProducts.AddAsync(openLoopProduct1, CancellationToken)).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<UpdateOpenLoopProductCommand>()
                .With(x => x.OpenLoopProductId, entity.Id)
                .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.OpenLoopProduct.Should().NotBeNull();
            actual.OpenLoopProduct.Id.Should().Be(entity.Id);
            actual.OpenLoopProduct.IsActive.Should().BeFalse();
            actual.OpenLoopProduct.UpdatedDateTime.Should().Be(entity.UpdatedDateTime);
        }

        [Fact]
        public async Task Given_Valid_Inactive_OpenLoopProduct_Should_Not_be_updated()
        {
            // Arrange
            var openLoopProduct1 = Fixture.Build<OpenLoopProduct>()
                                          .With(p => p.Id, new Guid(Constants.OpenLoopProductId1))
                                          .With(p => p.UpdatedDateTime, DateTime.Now)
                                          .With(p => p.IsActive, false)
                                          .Create();

            await DbContext.OpenLoopProducts.AddAsync(openLoopProduct1, CancellationToken);

            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<UpdateOpenLoopProductCommand>()
                .With(x => x.OpenLoopProductId, openLoopProduct1.Id)
                .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.OpenLoopProduct.Should().NotBeNull();
            actual.OpenLoopProduct.Id.Should().Be(openLoopProduct1.Id);
            actual.OpenLoopProduct.IsActive.Should().BeFalse();
            actual.OpenLoopProduct.UpdatedDateTime.Should().Be(openLoopProduct1.UpdatedDateTime);
        }

        [Fact]
        public async Task Given_Non_Existing_OpenLoopProduct_Should_Be_handled()
        {
            // Arrange
            var openLoopProduct1 = Fixture.Build<OpenLoopProduct>()
                                          .With(p => p.Id, new Guid(Constants.OpenLoopProductId1))
                                          .With(p => p.UpdatedDateTime, DateTime.Now)
                                          .With(p => p.IsActive, true)
                                          .Create();

            var request = Fixture.Build<UpdateOpenLoopProductCommand>()
                .With(x => x.OpenLoopProductId, openLoopProduct1.Id)
                .Create();
            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.OpenLoopProduct.Should().BeNull();
            actual.Message.Should().Be($"open loop product with this id {openLoopProduct1.Id} does not exists.");
        }
    }
}
