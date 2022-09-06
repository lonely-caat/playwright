using FluentValidation.TestHelper;
using System.Collections.Generic;
using Xunit;
using Zip.Api.CustomerSummary.Application.Communications.Query.GetEmailsSent;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Communications
{
    public class GetEmailsSentQueryValidatorTest
    {
        private readonly GetEmailsSentQueryValidator _validator;
        public GetEmailsSentQueryValidatorTest()
        {
            _validator = new GetEmailsSentQueryValidator();
        }

        [Fact]
        public void Given_EmailTypesEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.EmailTypes, new List<string>());
        }

        [Fact]
        public void Given_ConsumerIdEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, -1);
        }

        [Fact]
        public void Should_pass()
        {
            var testInput = new List<string>();
            testInput.Add("A");
            var result = _validator.Validate(new GetEmailsSentQuery()
            {
                ConsumerId = 123,
                EmailTypes = testInput
            });

            Assert.True(result.IsValid);
        }
    }
}
