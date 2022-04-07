using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;
using Zip.Core.Extensions;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateLoginStatus
{
    public class UpdateLoginStatusCommandHandler : BaseRequestHandler<UpdateLoginStatusCommand, UpdateLoginStatusResponse>
    {
        private readonly IMediator _mediator;
        private readonly ICustomerCoreService _customerCoreService;
        
        public UpdateLoginStatusCommandHandler(
            ILogger<UpdateLoginStatusCommandHandler> logger,
            IMediator mediator,
            ICustomerCoreService customerCoreService) : base(logger)
        {
            _mediator = mediator;
            _customerCoreService = customerCoreService;
        }

        protected override string HandlerName => nameof(UpdateLoginStatusCommandHandler);

        public override async Task<UpdateLoginStatusResponse> Handle(UpdateLoginStatusCommand request, CancellationToken cancellationToken)
        {
            var logSuffix = ($"for request: {request.ToJsonString()}");

            try
            {
                LogInformation($"Start processing {nameof(UpdateLoginStatusCommand)} {logSuffix}");

                var getContractRequest = new GetContactQuery(request.ConsumerId);

                var contact = await _mediator.Send(getContractRequest);

                if (string.IsNullOrEmpty(contact?.Email))
                {
                    throw new ContactNotFoundException($"Unable to find contract's email for existing customer with consumer id = ${request.ConsumerId}");
                }

                var loginStatusRequest = new UpdateLoginStatusRequest
                {
                    Email = contact.Email,
                    CreatedBy = request.ChangedBy
                };

                var loginStatusResponse = await UpdateLoginStatusAsync(loginStatusRequest, request.LoginStatusType, cancellationToken);

                LogInformation($"Successfully processed {nameof(UpdateLoginStatusCommand)} with Result: {loginStatusResponse?.ToJsonString()} {logSuffix}");
                return loginStatusResponse;
            }
            catch (Exception ex)
            {
                LogError(ex, $"Failed to process {nameof(UpdateLoginStatusCommand)} {logSuffix}");
                throw;
            }
        }

        private async Task<UpdateLoginStatusResponse> UpdateLoginStatusAsync(UpdateLoginStatusRequest request, LoginStatusType loginStatusType, CancellationToken cancellationToken)
        {
            var logSuffix = ($"for request: {request.ToJsonString()}");

            LogInformation($"Start processing {nameof(UpdateLoginStatusAsync)} to {loginStatusType} {logSuffix}");

            switch (loginStatusType)
            {
                case LoginStatusType.Enabled:
                    var enableLoginResponse = await _customerCoreService.EnableCustomerLoginAsync(request, cancellationToken);
                    return enableLoginResponse;

                case LoginStatusType.Disabled:
                    var disableLoginResponse = await _customerCoreService.DisableCustomerLoginAsync(request, cancellationToken);
                    return disableLoginResponse;

                default:
                    var errorMessage =
                        $"Failed to process {nameof(UpdateLoginStatusCommand)} with LoginStatusType of {loginStatusType} {logSuffix}";
                    LogError(errorMessage);
                    throw new CustomerCoreApiException(errorMessage);
            }
        }
    }
}