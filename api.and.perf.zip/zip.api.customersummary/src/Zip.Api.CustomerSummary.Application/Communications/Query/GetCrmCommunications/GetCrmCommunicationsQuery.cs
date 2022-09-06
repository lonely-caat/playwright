using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;

namespace Zip.Api.CustomerSummary.Application.Communications.Query.GetCrmCommunications
{
    public class GetCrmCommunicationsQuery : IRequest<IEnumerable<MessageLogDto>>
    {
        public MessageLogCategory MessageLogCategory { get; set; }
        
        public long ReferenceId { get; set; }
    }
}
