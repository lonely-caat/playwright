using System;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Reports.Query.GetMerchantDemographicReport;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.Reports
{
    public class GetMerchantDemographicReportQueryValidatorTests : CommonTestsFixture
    {
        private const string ValidABN = "48 123 123 124";
        private const string ValidPostcode = "2154";

        [Theory]
        [InlineData("")]
        [InlineData("333")]
        [InlineData("121212121212")]
        [InlineData("ABC")]
        [InlineData("AbnShouldNotHaveChar")]
        [InlineData("1111111111A")]
        public void Given_Invalid_ABN_Validator_Should_Have_Error(string abn)
        {
            // Arrange
            var validator = new GetMerchantDemographicReportQueryValidator();
            var query = Fixture.Build<GetMerchantDemographicReportQuery>()
                               .With(x => x.ABN, abn)
                               .With(x => x.Postcode, ValidPostcode)
                               .With(x => x.StartDateTime, DateTime.Now.AddMonths(-1))
                               .With(x => x.EndDateTime, DateTime.Now)
                               .Create();

            // Act & Assert
            validator.Validate(query).IsValid.Should().Be(false);
        }

        [Theory]
        [InlineData("")]
        [InlineData("333")]
        [InlineData("0199")]
        [InlineData("55555")]
        public void Given_Invalid_Postcode_Validator_Should_Have_Error(string postcode)
        {
            // Arrange
            var validator = new GetMerchantDemographicReportQueryValidator();
            var query = Fixture.Build<GetMerchantDemographicReportQuery>()
                               .With(x => x.ABN, ValidABN)
                               .With(x => x.Postcode, postcode)
                               .With(x => x.StartDateTime, DateTime.Now.AddMonths(-1))
                               .With(x => x.EndDateTime, DateTime.Now)
                               .Create();

            // Act & Assert
            validator.Validate(query).IsValid.Should().Be(false);
        }

        [Fact]
        public void Given_InValid_DateTime_Params_Validator_Should_Have_Error()
        {
            // Arrange
            var validator = new GetMerchantDemographicReportQueryValidator();
            var query = Fixture.Build<GetMerchantDemographicReportQuery>()
                               .With(x => x.ABN, ValidABN)
                               .With(x => x.Postcode, ValidPostcode)
                               .With(x => x.StartDateTime, DateTime.Now)
                               .With(x => x.EndDateTime, DateTime.Now.AddMonths(-1))
                               .Create();

            // Act & Assert
            validator.Validate(query).IsValid.Should().Be(false);
        }

        [Fact]
        public void Given_InValid_DateTime_Range_Validator_Should_Have_Error()
        {
            // Arrange
            var validator = new GetMerchantDemographicReportQueryValidator();
            var query = Fixture.Build<GetMerchantDemographicReportQuery>()
                               .With(x => x.ABN, ValidABN)
                               .With(x => x.Postcode, ValidPostcode)
                               .With(x => x.EndDateTime, DateTime.Now)
                               .With(x => x.StartDateTime, DateTime.Now.AddMonths(-13))
                               .Create();

            // Act & Assert
            validator.Validate(query).IsValid.Should().Be(false);
        }

        [Fact]
        public void Given_InValid_DemographicReportQueryTypes_Validator_Should_Have_Error()
        {
            // Arrange
            var validator = new GetMerchantDemographicReportQueryValidator();
            var query = Fixture.Build<GetMerchantDemographicReportQuery>()
                               .With(x => x.ABN, ValidABN)
                               .With(x => x.Postcode, ValidPostcode)
                               .With(x => x.StartDateTime, DateTime.Now.AddMonths(-1))
                               .With(x => x.EndDateTime, DateTime.Now)
                               .Create();
            query.DemographicReportQueryTypes = null;

            // Act & Assert
            validator.Validate(query).IsValid.Should().Be(false);
        }

        [Fact]
        public void Given_Valid_Query_Params_Validator_Should_Have_No_Error()
        {
            // Arrange
            var validator = new GetMerchantDemographicReportQueryValidator();
            var query = Fixture.Build<GetMerchantDemographicReportQuery>()
                               .With(x => x.ABN, ValidABN)
                               .With(x => x.Postcode, ValidPostcode)
                               .With(x => x.StartDateTime, DateTime.Now.AddMonths(-1))
                               .With(x => x.EndDateTime, DateTime.Now)
                               .Create();

            // Act & Assert
            validator.Validate(query).IsValid.Should().Be(true);
        }
    }
}
