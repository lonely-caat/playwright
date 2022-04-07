using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;

namespace Zip.Api.CustomerSummary.Application.Communications.Query.GetEmailsSent
{
    public class GetEmailsSentQuery : IRequest<IEnumerable<EmailSent>>
    {
        public long ConsumerId { get; set; }

        public List<string> EmailTypes { get; set; }
    }
}