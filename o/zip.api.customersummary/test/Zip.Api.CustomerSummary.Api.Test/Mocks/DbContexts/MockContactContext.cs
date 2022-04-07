using System;
using System.Threading.Tasks;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockContactContext : IContactContext
    {
        private readonly IFixture _fixture;

        public MockContactContext()
        {
            _fixture = new Fixture();
        }

        public Task<ContactDto> GetContactAsync(long consumerId)
        {
            var result = consumerId == int.MaxValue
                ? null
                : new ContactDto
                {
                    ConsumerId = _fixture.Create<long>(),
                    AuthorityDateOfBirth = _fixture.Create<DateTime>(),
                    AuthorityFullName = _fixture.Create<string>(),
                    AuthorityMobile = "0415808999",
                    Email = "shan.ke@zip.co",
                    Mobile = "0415801111",
                };

            return Task.FromResult(result);
        }

        public Task<string> GetMobileAsync(long consumerId)
        {
            var result = consumerId == int.MaxValue ? null : "0415808999";
            return Task.FromResult(result);
        }
    }
}
