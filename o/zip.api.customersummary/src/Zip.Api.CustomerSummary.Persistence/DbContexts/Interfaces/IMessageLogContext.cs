using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IMessageLogContext
    {
        Task<MessageLog> GetAsync(long id);

        Task<IEnumerable<MessageLogDto>> GetAsync(MessageLogCategory category, long referenceId);

        Task<IEnumerable<MessageLog>> GetEmailsSentAsync(long consumerId, List<int> type);

        Task<bool> InsertAsync(
            long referenceId,
            Guid messageId,
            string subject,
            string detail,
            MessageLogSettings settings,
            DateTime timeStamp,
            string messageBody = null);
    }
}