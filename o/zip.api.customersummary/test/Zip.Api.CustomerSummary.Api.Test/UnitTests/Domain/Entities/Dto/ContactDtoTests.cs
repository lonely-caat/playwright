using System;
using AutoFixture;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Dto
{
    public class ContactDtoTests
    {
        private readonly IFixture _fixture;

        public ContactDtoTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Should_equal()
        {
            var consumerId = _fixture.Create<long>();
            var email = _fixture.Create<string>();
            var mobile = _fixture.Create<string>();
            var fullName = _fixture.Create<string>();
            var amobile = _fixture.Create<string>();
            var dob = _fixture.Create<DateTime>();

            var result = new ContactDto()
            {
                ConsumerId = consumerId,
                Email = email,
                Mobile = mobile,
                AuthorityFullName = fullName,
                AuthorityMobile = amobile,
                AuthorityDateOfBirth = dob
            };

            Assert.Equal(consumerId, result.ConsumerId);
            Assert.Equal(email, result.Email);
            Assert.Equal(mobile, result.Mobile);
            Assert.Equal(amobile, result.AuthorityMobile);
            Assert.Equal(fullName, result.AuthorityFullName);
            Assert.Equal(dob, result.AuthorityDateOfBirth);
        }
    }
}
