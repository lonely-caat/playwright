using FluentValidation.TestHelper;
using System;
using Xunit;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetRewardActivity;
using Zip.Api.CustomerSummary.Domain.Common.Constants;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Beam
{
    public class GetRewardActivityQueryValidatorTests
    {
        private readonly GetRewardActivityQueryValidator _validator;

        public GetRewardActivityQueryValidatorTests()
        {
            _validator = new GetRewardActivityQueryValidator();
        }

        [Fact]
        public void Given_Valid_Parameters_Should_Be_Ok()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x, new GetRewardActivityQuery(Guid.NewGuid(), 1, 10, Regions.Australia));
        }

        [Fact]
        public void Given_Empty_CustomerId_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CustomerId, new GetRewardActivityQuery(Guid.Empty, 1, 10, Regions.Australia));
            _validator.ShouldHaveValidationErrorFor(x => x.PageNumber, new GetRewardActivityQuery(Guid.NewGuid(), 0, 10, Regions.Australia));
            _validator.ShouldHaveValidationErrorFor(x => x.PageSize, new GetRewardActivityQuery(Guid.NewGuid(), 1, 0, Regions.Australia));
        }
    }
}
