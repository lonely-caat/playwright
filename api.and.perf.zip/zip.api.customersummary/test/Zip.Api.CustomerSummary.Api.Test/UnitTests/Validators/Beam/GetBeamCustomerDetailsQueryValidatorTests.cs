using FluentValidation.TestHelper;
using System;
using Xunit;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetCustomerDetails;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Beam
{
    public class GetBeamCustomerDetailsQueryValidatorTests
    {
        private readonly GetCustomerDetailsQueryValidator _validator;

        public GetBeamCustomerDetailsQueryValidatorTests()
        {
            _validator = new GetCustomerDetailsQueryValidator();
        }

        [Fact]
        public void Given_Valid_Parameters_Should_Be_Ok()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.CustomerId, Guid.NewGuid());
        }

        [Fact]
        public void Given_Empty_CustomerId_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CustomerId, Guid.Empty);
        }
    }
}
