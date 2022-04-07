using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Mfa.Query;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Mfa
{
    public class GetMfaSmsDataQueryValidatorTests
    {
        private readonly GetMfaSmsDataQueryValidator _validator;

        public GetMfaSmsDataQueryValidatorTests()
        {
            _validator = new GetMfaSmsDataQueryValidator();
        }

        [Fact]
        public void Given_Valid_Parameters_Should_Be_Ok()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.ConsumerId, 1234);
        }

        [Fact]
        public void Given_ConsumerId_Is_0_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
        }
        
    }
}