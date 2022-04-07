using System.Collections.Generic;
using MediatR;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.SetConsumerAttributes
{
    public class SetConsumerAttributesCommand : IRequest
    {
        public long ConsumerId { get; set; }
      
        public List<long> Attributes { get; set; }
    }
}
