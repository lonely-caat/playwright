using System;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Beam.Query.PollReconciliationReport;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Beam
{
    public class PollReconciliationReportQueryValidatorTests
    {
        private readonly PollReconciliationReportQueryValidator _validator;

        public PollReconciliationReportQueryValidatorTests()
        {
            _validator = new PollReconciliationReportQueryValidator();
        }

        [Fact]
        public void Given_Valid_Parameters_Should_Be_Ok()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Uuid, new PollReconciliationReportQuery(Guid.NewGuid(), "test.email@zip.co"));
        }

        [Fact]
        public void Given_Empty_CustomerId_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Uuid, Guid.Empty);
        }
    }
}
