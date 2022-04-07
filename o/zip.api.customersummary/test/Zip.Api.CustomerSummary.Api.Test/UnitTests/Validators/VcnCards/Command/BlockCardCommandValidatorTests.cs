using System;
using AutoFixture;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockCard;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.VcnCards.Command
{
    public class BlockCardCommandValidatorTests : CommonTestsFixture
    {
        private readonly BlockCardCommandValidator _validator;

        public BlockCardCommandValidatorTests()
        {
            _validator = new BlockCardCommandValidator();
        }

        [Fact]
        public void Given_Valid_CardId_Should_Not_Have_Error()
        {
            var result = _validator.Validate(new BlockCardCommand
            {
                CardId = Fixture.Create<Guid>()
            });

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Empty_CardId_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CardId, Guid.Empty);
        }
    }
}
