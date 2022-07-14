using System;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Products.Command.DeleteMerchantProduct;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.Products
{
    public class DeleteMerchantProductCommandValidatorTests : CommonTestsFixture
    {
        private readonly DeleteMerchantProductCommandValidator _validator;

        public DeleteMerchantProductCommandValidatorTests()
        {
            _validator = new DeleteMerchantProductCommandValidator();
        }

        [Fact]
        public void Should_Be_Valid()
        {
            // Arrange
            var request = Fixture.Build<DeleteMerchantProductCommand>()
                                 .With(x => x.MerchantOpenLoopProductId, Guid.NewGuid)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Be_Invalid()
        {
            // Arrange
            var request = Fixture.Build<DeleteMerchantProductCommand>()
                                 .Without(x => x.MerchantOpenLoopProductId)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeFalse();
        }
    }
}