using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendPaidOutAndClosedEmail
{
    public class SendPaidOutAndClosedEmailCommandHandler : IRequestHandler<SendPaidOutAndClosedEmailCommand, bool>
    {
        private readonly ICommunicationsService _paidOutCloseService;
        private readonly IMediator _mediator;
        private readonly ICreditProfileContext _creditProfileContext;

        public SendPaidOutAndClosedEmailCommandHandler(ICommunicationsService paidOutCloseService,
                                                        IMediator mediator,
                                                        ICreditProfileContext creditProfileContext)
        {
            _paidOutCloseService = paidOutCloseService ?? throw new ArgumentNullException(nameof(paidOutCloseService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _creditProfileContext = creditProfileContext ?? throw new ArgumentNullException(nameof(creditProfileContext));
        }

        public async Task<bool> Handle(SendPaidOutAndClosedEmailCommand request, CancellationToken cancellationToken)
        {
            PaidOutAndClosedEmail payload = new PaidOutAndClosedEmail();
            Consumer consumerDetails = null;
            GetAccountInfoQueryResult accountDetails = null;
            ContactDto contactDetails = null;
            string dateofClosure = string.Empty;

            Task[] tasks = new Task[]
                            {
                                _mediator.Send(new GetConsumerQuery(request.ConsumerId)).ContinueWith((TResult) => consumerDetails = TResult.Result),
                                _mediator.Send(new GetContactQuery(request.ConsumerId)).ContinueWith((TResult) => contactDetails = TResult.Result),
                                _mediator.Send(new GetAccountInfoQuery(request.ConsumerId)).ContinueWith((TResult) => accountDetails = TResult.Result),
                                _creditProfileContext.GetProfileDateOfClosureAsync(request.ConsumerId).ContinueWith((TResult) => dateofClosure = TResult.Result)
                            };

            await Task.WhenAll(tasks);

            if (consumerDetails != null && accountDetails != null && contactDetails != null)
            {
                payload.ConsumerId = consumerDetails.ConsumerId;
                payload.FullName = consumerDetails.FirstName + " " + consumerDetails.LastName;
                payload.Address = consumerDetails.Address.FullAddress;
                payload.AccountId = accountDetails.AccountInfo.AccountId;
                payload.Product = accountDetails.AccountInfo.Product.Value.ToString();
                payload.Email = contactDetails.Email;
                payload.DateOfClosure = dateofClosure;
                payload.DateOfLetterGeneration = DateTime.Now.ToString("dd/MM/yyyy");

                CommunicationApiResponse response = await _paidOutCloseService.SendPaidOutCloseEmailAsync(payload);
                return response.Success;
            }

            return false;
        }
    }
}