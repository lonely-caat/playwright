using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateEmail;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateMobile;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
    {
        private readonly IMediator _mediator;

        public UpdateContactCommandHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var consumer = await _mediator.Send(new GetConsumerQuery(request.ConsumerId), cancellationToken);

            if (consumer == null)
            {
                throw new ConsumerNotFoundException(request.ConsumerId);
            }

            var currentContact = await _mediator.Send(new GetContactQuery(request.ConsumerId), cancellationToken);

            var currentAccount = await _mediator.Send(new GetAccountInfoQuery(request.ConsumerId), cancellationToken);

            if (currentAccount?.AccountInfo == null)
            {
                throw new AccountNotFoundException(request.ConsumerId, default);
            }

            if (currentContact.Email != request.Email)
            {
                await _mediator.Send(new UpdateEmailCommand(request.Email, consumer, currentAccount.AccountInfo), cancellationToken);
            }

            if (currentContact.Mobile != request.Mobile)
            {
                await _mediator.Send(new UpdateMobileCommand(request.Mobile, consumer, currentAccount.AccountInfo), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
