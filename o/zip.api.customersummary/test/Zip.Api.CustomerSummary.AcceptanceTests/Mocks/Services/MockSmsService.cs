using AutoFixture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockSmsService : ISmsService
    {
        private readonly IFixture _fixture;

        public bool Sent { get; set; }
        
        public MockSmsService()
        {
            _fixture = new Fixture();
        }

        public Task<SmsContent> GetSmsContentAsync(int id)
        {
            return Task.FromResult(Create());
        }

        private SmsContent Create()
        {
            return new SmsContent
            {
                Active = _fixture.Create<bool>(),
                Content = _fixture.Create<string>(),
                Id = _fixture.Create<int>(),
                Name = _fixture.Create<string>(),
                TimeStamp = _fixture.Create<DateTime>()
            };
        }

        private SmsResponse CreateResponse()
        {
            return new SmsResponse
            {
                Success = true
            };
        }

        public Task<SmsContent> GetSmsContentByNameAsync(string name)
        {
            return Task.FromResult(Create());
        }

        public SmsResponse SendAsync(string mobilePhone, string message, Dictionary<string, string> replacementValues = null)
        {
            return CreateResponse();
        }

        public SmsResponse SendSmsToConsumer(string phoneNumber, string message)
        {
            return CreateResponse();
        }
    }
}
