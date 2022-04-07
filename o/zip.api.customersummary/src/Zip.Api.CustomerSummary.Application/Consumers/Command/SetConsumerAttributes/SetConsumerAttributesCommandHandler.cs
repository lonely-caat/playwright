using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.SetConsumerAttributes
{
    public class SetConsumerAttributesCommandHandler : IRequestHandler<SetConsumerAttributesCommand>
    {
        private readonly IAttributeContext _attributeContext;

        public SetConsumerAttributesCommandHandler(
            IAttributeContext attributeContext)
        {
            _attributeContext = attributeContext ?? throw new ArgumentNullException(nameof(attributeContext));
        }
        
        public async Task<Unit> Handle(SetConsumerAttributesCommand request, CancellationToken cancellationToken)
        {
            await _attributeContext.SetConsumerAttributesAsync(request.ConsumerId, request.Attributes);
            
            return Unit.Value;
        }
    }
}
