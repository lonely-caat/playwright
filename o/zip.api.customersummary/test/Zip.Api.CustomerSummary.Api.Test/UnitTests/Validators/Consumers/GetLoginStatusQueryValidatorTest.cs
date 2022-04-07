using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetLoginStatus;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Consumers
{
    public class GetLoginStatusQueryValidatorTest : CommonTestsFixture
    {
        private readonly GetLoginStatusQueryValidator _validator;

        public GetLoginStatusQueryValidatorTest()
        {
            _validator = new GetLoginStatusQueryValidator();
        }

        [Fact]
        public void Given_Valid_Parameters_Should_Be_Ok()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.ConsumerEmail, "johnny.vuong@zip.co");
        }

        [Fact]
        public void Given_Invalid_Parameters_Should_Be_Caught()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerEmail, "NotAValidEmail");
        }
    }
}
