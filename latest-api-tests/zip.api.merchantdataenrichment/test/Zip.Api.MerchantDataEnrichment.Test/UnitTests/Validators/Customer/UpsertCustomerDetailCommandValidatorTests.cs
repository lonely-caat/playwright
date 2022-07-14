using System;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Customer.Command.UpsertCustomerDetail;
using Zip.MerchantDataEnrichment.Domain.Enums;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.Customer
{
    public class UpsertCustomerDetailCommandValidatorTests : CommonTestsFixture
    {
        private readonly UpsertCustomerDetailCommandValidator _validator;

        public UpsertCustomerDetailCommandValidatorTests()
        {
            _validator = new UpsertCustomerDetailCommandValidator();
        }

        [Fact]
        public void Test_Empty_UpsertCustomerDetailCommand()
        {
            // Arrange
            var command = Fixture.Build<UpsertCustomerDetailCommand>()
                                 .With(x => x.AccountId, default(long))
                                 .With(x => x.CustomerId, string.Empty)
                                 .With(x => x.ConsumerId, default(long))
                                 .With(x => x.PublicConsumerId, string.Empty)
                                 .With(x => x.Email, string.Empty)
                                 .With(x => x.Gender, default(Gender))
                                 .With(x => x.DateOfBirth, default(DateTime))
                                 .With(x => x.CountryId, string.Empty)
                                 .With(x => x.CountryCode, string.Empty)
                                 .With(x => x.CreditProfileStateType, default(CreditProfileStateType))
                                 .With(x => x.RecentUpdatedDateTime, default(DateTime))
                                 .Create();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().Be(false);
            result.Errors.Count.Should().Be(12);
        }

        [Theory]
        [InlineData("8f4d9890-bd75-46a9-82b2-9cf6a8b68696", "AU", 0, true)]
        [InlineData("8f4d9890", "AUS", 3, false)]
        public void Test_UpsertCustomerDetailCommand(string customerId, string countryId, int errorCount, bool isValid)
        {
            // Arrange
            var validator = new UpsertCustomerDetailCommandValidator();
            var command = Fixture.Build<UpsertCustomerDetailCommand>()
                                 .With(x => x.CustomerId, customerId)
                                 .With(x => x.CountryId, countryId)
                                 .With(x => x.CountryCode, countryId)
                                 .Create();

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().Be(isValid);
            Assert.Equal(errorCount, result.Errors.Count);
        }
    }
}
