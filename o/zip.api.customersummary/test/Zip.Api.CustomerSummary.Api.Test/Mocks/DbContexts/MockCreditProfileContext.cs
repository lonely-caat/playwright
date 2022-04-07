using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockCreditProfileContext : ICreditProfileContext
    {
        private readonly IFixture _fixture;

        public MockCreditProfileContext()
        {
            _fixture = new Fixture();
        }

        public Task CreateCreditProfileAttributeAsync(long creditProfileId, long profileAttributeId)
        {
            return Task.FromResult(0);
        }

        public Task CreateCreditProfileClassificationAsync(long creditProfileId, long profileClassificationId)
        {
            return Task.FromResult(0);
        }

        public Task CreateCreditProfileStateAsync(CreditProfileState creditProfileState)
        {
            return Task.FromResult(0);

        }

        public Task<IEnumerable<ProfileAttribute>> GetProfileAttributesAsync(CreditProfileStateType creditProfileStateType)
        {
            return Task.FromResult<IEnumerable<ProfileAttribute>>(new List<ProfileAttribute>
            {
                new ProfileAttribute
                {
                    Id = _fixture.Create<long>(),
                    Description = _fixture.Create<string>(),
                    Active = _fixture.Create<bool>(),
                    TimeStamp = _fixture.Create<DateTime>(),
                    Type = _fixture.Create<ProfileStateType>()
                },
                new ProfileAttribute
                {
                    Id = _fixture.Create<long>(),
                    Description = _fixture.Create<string>(),
                    Active = _fixture.Create<bool>(),
                    TimeStamp = _fixture.Create<DateTime>(),
                    Type = _fixture.Create<ProfileStateType>()
                },
                new ProfileAttribute
                {
                    Id = _fixture.Create<long>(),
                    Description = _fixture.Create<string>(),
                    Active = _fixture.Create<bool>(),
                    TimeStamp = _fixture.Create<DateTime>(),
                    Type = _fixture.Create<ProfileStateType>()
                }
            });
        }

        public Task<IEnumerable<ProfileClassification>> GetProfileClassificationsAsync(CreditProfileStateType creditProfileStateType)
        {
            return Task.FromResult<IEnumerable<ProfileClassification>>(new List<ProfileClassification>
            {
                new ProfileClassification
                {
                    Id = _fixture.Create<long>(),
                    Description = _fixture.Create<string>(),
                    Active = _fixture.Create<bool>(),
                    TimeStamp = _fixture.Create<DateTime>(),
                    Type = _fixture.Create<ProfileStateType>(),
                },
                new ProfileClassification
                {
                    Id = _fixture.Create<long>(),
                    Description = _fixture.Create<string>(),
                    Active = _fixture.Create<bool>(),
                    TimeStamp = _fixture.Create<DateTime>(),
                    Type = _fixture.Create<ProfileStateType>()
                },
                new ProfileClassification
                {
                    Id = _fixture.Create<long>(),
                    Description = _fixture.Create<string>(),
                    Active = _fixture.Create<bool>(),
                    TimeStamp = _fixture.Create<DateTime>(),
                    Type = _fixture.Create<ProfileStateType>()
                }
            });
        }

        public Task<string> GetProfileDateOfClosureAsync(long consumerId)
        {
            var result = consumerId == int.MaxValue ? null : "10/01/2020";
            return Task.FromResult(result);
        }

        public Task<CreditProfileState> GetStateTypeByConsumerIdAsync(long consumerId)
        {
            var result = consumerId == int.MaxValue
                ? null
                : new CreditProfileState
                {
                    ChangedBy = _fixture.Create<string>(),
                    Comments = _fixture.Create<string>(),
                    CreditProfileId = _fixture.Create<long>(),
                    CreditStateType = _fixture.Create<CreditProfileStateType>(),
                    Id = _fixture.Create<long>(),
                    ReasonCode = _fixture.Create<CreditProfileStateReasonCode>(),
                    TimeStamp = _fixture.Create<DateTime>()
                };

            return Task.FromResult(result);
        }
    }
}
