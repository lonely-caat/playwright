using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts
{
    public class MockMessageLogContext : IMessageLogContext
    {
        private readonly IFixture _fixture;

        public MockMessageLogContext()
        {
            _fixture = new Fixture();
        }

        public async Task<MessageLog> GetAsync(long id)
        {
            return await Task.FromResult(_fixture.Create<MessageLog>());
        }
        
        public async Task<IEnumerable<MessageLogDto>> GetAsync(MessageLogCategory category, long referenceId)
        {
            var result = referenceId == int.MaxValue ? null : _fixture.Create<IEnumerable<MessageLogDto>>();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<MessageLog>> GetEmailsSentAsync(long consumerId, List<int> type)
        {
            return await Task.FromResult(_fixture.Create<IEnumerable<MessageLog>>());
        }

        public async Task<bool> InsertAsync(
            long referenceId,
            Guid messageId,
            string subject,
            string detail,
            MessageLogSettings settings,
            DateTime timeStamp,
            string messageBody = null)
        {
            if(referenceId == int.MaxValue)
                throw new Exception("test exception");
            return await Task.FromResult(true);
        }
    }
}
