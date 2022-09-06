using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateLoginStatus;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Consumers
{
    public class UpdateLoginStatusCommandValidatorTest : CommonTestsFixture
    {
        private readonly UpdateLoginStatusCommandValidator _validator;
        
        public UpdateLoginStatusCommandValidatorTest()
        {
            _validator = new UpdateLoginStatusCommandValidator();
        }

        [Fact]
        public void Given_Valid_LoginStatus_Parameters_Should_Be_Ok()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.LoginStatusType, LoginStatusType.Disabled);
            _validator.ShouldNotHaveValidationErrorFor(x => x.LoginStatusType, LoginStatusType.Enabled);
            _validator.ShouldNotHaveValidationErrorFor(x => x.LoginStatusType, LoginStatusType.ResetRequired);
        }
        
        [Fact]
        public void Given_Invalid_Parameters_Should_Be_Caught()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
        }
    }
}
