using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Communications.Query.GetCrmCommunications
{
    public class GetCrmCommunicationsQueryHandler : IRequestHandler<GetCrmCommunicationsQuery, IEnumerable<MessageLogDto>>
    {
        private readonly IMessageLogContext _messageLogContext;

        public GetCrmCommunicationsQueryHandler(IMessageLogContext messageLogContext)
        {
            _messageLogContext = messageLogContext ?? throw new ArgumentNullException(nameof(messageLogContext));
        }
        
        public async Task<IEnumerable<MessageLogDto>> Handle(GetCrmCommunicationsQuery request, CancellationToken cancellationToken)
        {
            return await _messageLogContext.GetAsync(request.MessageLogCategory, request.ReferenceId);
        }
    }
}
