using System;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateConsumer;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Consumers
{
    public class UpdateConsumerCommandValidatorTest
    {
        private readonly UpdateConsumerCommandValidator _validator;

        public UpdateConsumerCommandValidatorTest()
        {
            _validator = new UpdateConsumerCommandValidator();
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, -1);
        }

        [Fact]
        public void Given_LessThan18_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.DateOfBirth, DateTime.Now.AddYears(-18));
        }

        [Fact]
        public void Should_pass()
        {
            var result = _validator.Validate(new UpdateConsumerCommand()
            {
                ConsumerId = 29382,
                DateOfBirth = DateTime.Now.Date.AddYears(-18).AddDays(-1),
                Address = new Address
                {
                    Country = new Country
                    {
                        Id = "AU",
                        Name = "Australia"
                    },
                    CountryCode = "AU",
                    PostCode = "2919",
                    State = "NSW",
                    StreetName = "Some St",
                    StreetNumber = "10",
                    Suburb = "Sydney"
                },
                CountryName = "Australia",
                FirstName = "Shan",
                LastName = "Ke",
                Gender = Gender.Female
            });

            Assert.True(result.IsValid);
        }
    }
}
