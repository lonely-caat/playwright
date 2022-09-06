using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Communications.Query.GetEmailsSent
{
    public class GetEmailsSentQueryHandler : IRequestHandler<GetEmailsSentQuery, IEnumerable<EmailSent>>
    {
        private readonly IMessageLogContext _messageLogContext;

        public GetEmailsSentQueryHandler(IMessageLogContext messageLogContext)
        {
            _messageLogContext = messageLogContext ?? throw new ArgumentNullException(nameof(messageLogContext));
        }

        public async Task<IEnumerable<EmailSent>> Handle(GetEmailsSentQuery request, CancellationToken cancellationToken)
        {
            var notificationType = request.EmailTypes
                                          .Where(x => Enum.IsDefined(typeof(NotificationType), x))
                                          .Select(x => (int)Enum.Parse(typeof(NotificationType), x, true))
                                          .ToList();
            
            if (!notificationType.Any())
            {
                return null;
            }

            var messageLog = await _messageLogContext.GetEmailsSentAsync(request.ConsumerId, notificationType);

            return messageLog?.Select(x => new EmailSent
            {
                ConsumerId = request.ConsumerId,
                EmailType = x.Type.ToString(),
                Email = x.Subject,
                CreatedDateTime = x.TimeStamp,
                Success = true
            });
        }
    }
}