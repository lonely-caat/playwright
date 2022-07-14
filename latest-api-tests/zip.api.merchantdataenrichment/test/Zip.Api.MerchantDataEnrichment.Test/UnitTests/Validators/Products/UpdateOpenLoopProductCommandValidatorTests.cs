using System;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Products.Command.UpdateOpenLoopProduct;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.Products
{
    public class UpdateOpenLoopProductCommandValidatorTests : CommonTestsFixture
    {
        [Fact]
        public void Given_A_Valid_Guid_Should_Not_Have_Error()
        {
            var validator = new UpdateOpenLoopProductCommandValidator();

            var cmd = Fixture.Build<UpdateOpenLoopProductCommand>()
                              .With(p => p.OpenLoopProductId, Guid.NewGuid())
                              .Create();

            validator.TestValidate(cmd).IsValid.Should().Be(true);
        }

        [Fact]
        public void Given_A_Valid_Guid_Should_Have_Error()
        {
            var validator = new UpdateOpenLoopProductCommandValidator();

            var cmd = Fixture.Build<UpdateOpenLoopProductCommand>()
                              .With(p => p.OpenLoopProductId, Guid.Empty)
                              .Create();

            validator.TestValidate(cmd).IsValid.Should().Be(false);
        }
    }
}
