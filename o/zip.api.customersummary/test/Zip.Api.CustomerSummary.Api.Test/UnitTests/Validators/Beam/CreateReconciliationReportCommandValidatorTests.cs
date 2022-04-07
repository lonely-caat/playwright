using AutoFixture;
using FluentValidation.TestHelper;
using System;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Beam.Command.CreateBeamReconciliationReport;
using Zip.Api.CustomerSummary.Application.Beam.Command.CreateReconciliationReport;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Beam
{
    public class CreateReconciliationReportCommandValidatorTests : CommonTestsFixture
    {
        private readonly CreateReconciliationReportCommandValidator _validator;

        public CreateReconciliationReportCommandValidatorTests()
        {
            _validator = new CreateReconciliationReportCommandValidator();
        }

        [Fact]
        public void Given_Valid_Parameters_Should_Be_Ok()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.SelectedDate, new DateTime());
        }
    }
}
