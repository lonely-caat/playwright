using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Communications.Command.InsertCrmCommunication;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Communications
{
    public class InsertCrmCommunicationCommandValidatorTest
    {
        private readonly InsertCrmCommunicationCommandValidator _validator;
        public InsertCrmCommunicationCommandValidatorTest()
        {
            
            _validator = new InsertCrmCommunicationCommandValidator();
        }

        [Fact]
        public void Given_ReferenceIdEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ReferenceId, -1);
        }

        [Fact]
        public void Given_SubjectEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Subject, "");
        }

        [Fact]
        public void Given_DetailEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Detail, (string) null);
        }
    }
}
