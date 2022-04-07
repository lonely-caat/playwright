using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;
using Zip.Core.Extensions;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetLoginStatus
{
    public class GetLoginStatusQueryHandler : BaseRequestHandler<GetLoginStatusQuery, LoginStatusResponse>
    {
        private readonly ICustomerCoreService _customerCoreService;
        
        public GetLoginStatusQueryHandler(
            ILogger<GetLoginStatusQueryHandler> logger,
            ICustomerCoreService customerCoreService) : base(logger)
        {
            _customerCoreService = customerCoreService;
        }

        protected override string HandlerName => nameof(GetLoginStatusQueryHandler);

        public override async Task<LoginStatusResponse> Handle(GetLoginStatusQuery request, CancellationToken cancellationToken)
        {
            var logSuffix = ($"for request: {request.ToJsonString()}");

            LogInformation($"Start processing {nameof(GetLoginStatusQuery)} {logSuffix}");

            try
            {
                var getLoginStatusRequest = new GetLoginStatusRequest()
                {
                    Email = request.ConsumerEmail
                };

                var response = await _customerCoreService.GetCustomerLoginStatusAsync(getLoginStatusRequest, cancellationToken);
                LogInformation($"Successfully processed {nameof(GetLoginStatusQuery)} with Result: {response?.ToJsonString()} {logSuffix}");
                return response.Data;
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetLoginStatusQuery), $"Failed to process {nameof(GetLoginStatusQuery)} {logSuffix}");
                throw;
            }
        }
    }
}