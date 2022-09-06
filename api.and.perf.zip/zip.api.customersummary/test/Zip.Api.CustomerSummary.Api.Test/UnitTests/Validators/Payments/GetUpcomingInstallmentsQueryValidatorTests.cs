using FluentValidation.TestHelper;
using System;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetUpcomingInstallments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Payments
{
    public class GetUpcomingInstallmentsQueryValidatorTests
    {
        private readonly GetUpcomingInstallmentsQueryValidator _validator;

        public GetUpcomingInstallmentsQueryValidatorTests()
        {
            _validator = new GetUpcomingInstallmentsQueryValidator();
        }

        [Fact]
        public void Given_Valid_Parameters_Should_Be_Ok()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.AccountId, 123);
            _validator.ShouldNotHaveValidationErrorFor(x => x.CustomerId, Guid.NewGuid());
            _validator.ShouldNotHaveValidationErrorFor(x => x.ToDate, DateTime.Now);
        }

        [Fact]
        public void Given_Invalid_Parameters_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.CustomerId, Guid.Empty);
        }
    }
}
