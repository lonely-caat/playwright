using AutoFixture;
using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
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
            return Task.FromResult(new ContactDto
            {
                ConsumerId = _fixture.Create<long>(),
                AuthorityDateOfBirth = _fixture.Create<DateTime>(),
                AuthorityFullName = _fixture.Create<string>(),
                AuthorityMobile = "0415808999",
                Email = "shan.ke@zip.co",
                Mobile = "0415801111",
            });
        }

        public Task<string> GetMobileAsync(long consumerId)
        {
            return Task.FromResult("0415808999");
        }
    }
}
