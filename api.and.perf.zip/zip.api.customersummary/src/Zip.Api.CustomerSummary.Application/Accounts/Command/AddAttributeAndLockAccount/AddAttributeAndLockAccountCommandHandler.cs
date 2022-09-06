using AutoMapper;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount;
using Zip.Api.CustomerSummary.Application.Consumers.Command.SetConsumerAttributes;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetAttributes;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumerAttributes;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.AddAttributeAndLockAccount
{
    public class AddAttributeAndLockAccountCommandHandler : IRequestHandler<AddAttributeAndLockAccountCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddAttributeAndLockAccountCommandHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(AddAttributeAndLockAccountCommand request, CancellationToken cancellationToken)
        {
            // Find intended attribute to add
            var allAttributes = await _mediator.Send(new GetAttributesQuery(), cancellationToken);

            var attributeToAdd = allAttributes.FirstOrDefault(x => x.Name.ToUpper() == request.Attribute.ToUpper());

            if (attributeToAdd == null)
            {
                throw new NotSupportedException($"Unable to find {request.Attribute} in the attribute list");
            }

            // Get Consumer's existing attributes
            var attributes = (await _mediator.Send(new GetConsumerAttributesQuery { ConsumerId = request.ConsumerId }, cancellationToken))?.ToList();

            // Make new attributes list
            var modifiedAttributes = new List<long>();

            if (attributes != null && attributes.Any())
            {
                modifiedAttributes.AddRange(attributes.Select(x => x.Id));
            }

            if (attributeToAdd != null)
            {
                modifiedAttributes.Add(attributeToAdd.Id);
            }

            Log.Information("{controller} :: {action} : {message}",
                            nameof(AddAttributeAndLockAccountCommandHandler),
                            nameof(Handle),
                            "Attributes retrieved and new attribute added to list");

            var setConsumerAttributesCommand = new SetConsumerAttributesCommand
            {
                Attributes = modifiedAttributes,
                ConsumerId = request.ConsumerId
            };
            await _mediator.Send(setConsumerAttributesCommand, cancellationToken);


            // Lock account
            var lockAccountModel = _mapper.Map<LockAccountCommand>(request);
            await _mediator.Send(lockAccountModel, CancellationToken.None);

            return Unit.Value;
        }            
    }       
}
