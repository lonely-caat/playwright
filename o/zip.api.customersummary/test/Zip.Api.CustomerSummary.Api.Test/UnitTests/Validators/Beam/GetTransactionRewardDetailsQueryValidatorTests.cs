using System;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetTransactionRewardDetails;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Beam
{
    public class GetTransactionRewardDetailsQueryValidatorTests
    {
        private readonly GetTransactionRewardDetailsQueryValidator _validator;

        public GetTransactionRewardDetailsQueryValidatorTests()
        {
            _validator = new GetTransactionRewardDetailsQueryValidator();
        }

        [Fact]
        public void Given_Valid_Parameters_Should_Be_Ok()
        {
            var query = new GetTransactionRewardDetailsQuery
            {
                CustomerId = Guid.NewGuid(),
                TransactionId = 12345
            };


            _validator.ShouldNotHaveValidationErrorFor(x => x.CustomerId, query);
            _validator.ShouldNotHaveValidationErrorFor(x => x.TransactionId, query);
        }

        [Fact]
        public void Given_Invalid_Parameters_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.CustomerId, Guid.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.TransactionId, 0);
        }
    }
}
