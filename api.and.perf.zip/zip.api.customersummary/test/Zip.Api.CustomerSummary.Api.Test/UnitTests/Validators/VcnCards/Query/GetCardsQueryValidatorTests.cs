using System;
using AutoFixture;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCards;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.VcnCards.Query
{
    public class GetCardsQueryValidatorTests : CommonTestsFixture
    {
        private readonly GetCardsQueryValidator _validator;

        public GetCardsQueryValidatorTests()
        {
            _validator = new GetCardsQueryValidator();
        }

        [Fact]
        public void Given_Valid_CustomerId_Should_Not_Have_Error()
        {
            var result = _validator.Validate(new GetCardsQuery
            {
                CustomerId = Fixture.Create<Guid>()
            });

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Empty_CustomerId_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CustomerId, Guid.Empty);
        }
    }
}
