using System;
using System.Globalization;
using System.Threading.Tasks;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerProfileService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockCustomerProfileService : ICustomerProfileService
    {
        private readonly IFixture _fixture;

        public MockCustomerProfileService()
        {
            _fixture = new Fixture();
        }

        public Task<ConsumerPersonalInfo> GetConsumerPersonalInfo(string searchKey)
        {
            return Task.FromResult(new ConsumerPersonalInfo
            {
                FirstName = _fixture.Create<string>(),
                LastName = _fixture.Create<string>(),
                DateOfBirth = _fixture.Create<DateTime>().ToString(CultureInfo.InvariantCulture),
                Gender = _fixture.Create<Gender>(),
                Address = _fixture.Create<Address>(),
                DriverLicence = _fixture.Create<DriverLicence>(),
            });
        }
    }
}