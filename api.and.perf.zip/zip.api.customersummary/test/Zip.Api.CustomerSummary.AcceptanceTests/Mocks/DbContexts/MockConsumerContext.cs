using AutoFixture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
{
    public class MockConsumerContext : IConsumerContext
    {
        private readonly IFixture _fixture;

        public MockConsumerContext()
        {
            _fixture = new Fixture();
        }

        public Task<AccountInfo> GetAccountInfoAsync(long consumerId)
        {
            return Task.FromResult<AccountInfo>(new AccountInfo
            {
                AccountId = _fixture.Create<long>(),
                AccountStatus = _fixture.Create<AccountStatus>(),
                ApplicationId = _fixture.Create<long>(),
                ConsumerId = consumerId,
                CustomerId = _fixture.Create<Guid>(),
                Product = _fixture.Create<ProductClassification>()
            });
        }

        public Task<Consumer> GetAsync(long consumerId)
        {
            return Task.FromResult<Consumer>(new Consumer
            {
                ConsumerId = consumerId,
                DateOfBirth = new DateTime(1988, 8, 8),
                FirstName = "Shan",
                LastName = "Ke",
                Address = new Address
                {
                    Country = new Country
                    {
                        Id = "AU",
                        Name = "Australia"
                    },
                    CountryCode = "AU",
                    PostCode = "2079",
                    State = "NSW",
                    StreetName = "Lady Street",
                    StreetNumber = "43",
                    Suburb = "Mount Colah"
                },
                Documents = new List<Document>
                {
                    new Document
                    {
                        Id = 123,
                        ConsumerId = consumerId,
                        DocumentType = DocumentType.DriverLicense,
                        Value1 = "22001928"
                    },
                    new Document
                    {
                        Id= 234,
                        ConsumerId = consumerId
                    }
                },
                Country = new Country
                {
                    Id = "AU",
                    Name = "Australia"
                },
                CountryId = "AU"

            });
        }

        public Task<Consumer> GetByCustomerIdAndProductAsync(Guid customerId, int product)
        {
            return Task.FromResult<Consumer>(new Consumer
            {
                CustomerId = customerId,
                ConsumerId = 123456,
                DateOfBirth = new DateTime(1988, 8, 8),
                FirstName = "Shan",
                LastName = "Ke",
                Address = new Address
                {
                    Country = new Country
                    {
                        Id = "AU",
                        Name = "Australia"
                    },
                    CountryCode = "AU",
                    PostCode = "2079",
                    State = "NSW",
                    StreetName = "Lady Street",
                    StreetNumber = "43",
                    Suburb = "Mount Colah"
                },
                Documents = new List<Document>
                {
                    new Document
                    {
                        Id = 123,
                        ConsumerId = 123456,
                        DocumentType = DocumentType.DriverLicense,
                        Value1 = "22001928"
                    },
                    new Document
                    {
                        Id= 234,
                        ConsumerId = 123456
                    }
                },
                Country = new Country
                {
                    Id = "AU",
                    Name = "Australia"
                },
                CountryId = "AU"

            });
        }

        public Task<IEnumerable<Document>> GetDocumentsAsync(long consumerId)
        {
            return Task.FromResult<IEnumerable<Document>>(new List<Document>
            {
                new Document
                {
                    Id = _fixture.Create<long>(),
                    ConsumerId = consumerId,
                    DocumentType = _fixture.Create<DocumentType>(),
                    FriendlyName = _fixture.Create<string>(),
                    Value1 = _fixture.Create<string>(),
                    Value2 = _fixture.Create<string>()
                },
                new Document
                {
                    Id = _fixture.Create<long>(),
                    ConsumerId = consumerId,
                    DocumentType = _fixture.Create<DocumentType>(),
                    FriendlyName = _fixture.Create<string>(),
                    Value1 = _fixture.Create<string>(),
                    Value2 = _fixture.Create<string>()
                }
            });
        }

        public Task<long?> GetLinkedConsumerIdAsync(long consumerId)
        {
            return Task.FromResult<long?>(382);
        }

        public Task UpdateDateOfBirthAsync(long consumerId, DateTime dateOfBirth)
        {
            return Task.FromResult(0);
        }

        public Task UpdateGenderAsync(long consumerId, Gender gender)
        {
            return Task.FromResult(0);
        }

        public Task UpdateNameAsync(long consumerId, string firstName, string lastName, string middleName, string otherName)
        {
            return Task.FromResult(0);
        }

        public Task SetTrustScoreAsync(long consumerId, int trustScore)
        {
            return Task.FromResult(0);
        }

        public Task<Consumer> GetAsyncV2(long consumerId)
        {
            return Task.FromResult<Consumer>(new Consumer
            {
                ConsumerId = consumerId,
                Documents = new List<Document>
                {
                    new Document
                    {
                        Id = 123,
                        ConsumerId = consumerId,
                        DocumentType = DocumentType.DriverLicense,
                        Value1 = "22001928"
                    },
                    new Document
                    {
                        Id= 234,
                        ConsumerId = consumerId
                    }
                },
                Country = new Country
                {
                    Id = "AU",
                    Name = "Australia"
                },
                CountryId = "AU"
            });
        }
    }
}
