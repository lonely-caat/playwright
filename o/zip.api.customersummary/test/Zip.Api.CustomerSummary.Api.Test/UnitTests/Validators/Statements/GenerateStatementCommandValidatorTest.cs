using System;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Statements.Command.GenerateStatement;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Statements
{
    public class GenerateStatementCommandValidatorTest
    {
        private readonly GenerateStatementCommandValidator _validator;

        public GenerateStatementCommandValidatorTest()
        {
            _validator = new GenerateStatementCommandValidator();
        }

        [Fact]
        public void Given_AccountIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, -292);
        }

        [Fact]
        public void Given_AllGood_Should_pass()
        {
            var result = _validator.Validate(new GenerateStatementCommand()
            {
                AccountId = 291,
                StartDate = DateTime.Now.AddMonths(-6),
                EndDate = DateTime.Now.AddMonths(-3)
            });

            Assert.True(result.IsValid);
        }
    }
}
