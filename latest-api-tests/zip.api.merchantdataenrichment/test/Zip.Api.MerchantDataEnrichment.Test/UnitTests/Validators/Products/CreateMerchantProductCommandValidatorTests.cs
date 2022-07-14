using System;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Products.Command.CreateMerchantProduct;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.Products
{
    public class CreateMerchantProductCommandValidatorTests : CommonTestsFixture
    {
        private readonly CreateMerchantProductCommandValidator _validator;

        public CreateMerchantProductCommandValidatorTests()
        {
            _validator = new CreateMerchantProductCommandValidator();
        }

        [Fact]
        public void Should_Be_Valid()
        {
            // Arrange
            var request = Fixture.Build<CreateMerchantProductCommand>()
                                 .With(x => x.ZipMerchantId, 1)
                                 .With(x => x.OpenLoopProductId, Guid.NewGuid)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Be_Invalid()
        {
            // Arrange
            var request = Fixture.Build<CreateMerchantProductCommand>()
                                 .With(x => x.ZipMerchantId, -1)
                                 .Without(x => x.OpenLoopProductId)
                                 .Create();

            // Act & Assert
            _validator.Validate(request).IsValid.Should().BeFalse();
        }
    }
}