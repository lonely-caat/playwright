using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService
{
    public class CustomerCoreService : BaseService<CustomerCoreService>, ICustomerCoreService
    {
        private readonly ICustomerCoreServiceProxy _customerCoreServiceProxy;

        public CustomerCoreService(
            ILogger<CustomerCoreService> logger,
            ICustomerCoreServiceProxy customerCoreServiceProxy) : base(logger)
        {
            _customerCoreServiceProxy = customerCoreServiceProxy;
        }

        public async Task<GetLoginStatusResponse> GetCustomerLoginStatusAsync(
            GetLoginStatusRequest request,
            CancellationToken cancellationToken)
        {
            var logSuffix = $"for request {request.ToJsonString()}";
            try
            {
                LogInformation(
                    nameof(GetCustomerLoginStatusAsync),
                    $"Start processing {nameof(GetCustomerLoginStatusAsync)} {logSuffix}");

                var httpResponseMessage = await _customerCoreServiceProxy.GetLoginStatusAsync(request.Email, cancellationToken);

                httpResponseMessage.EnsureSuccess();
                var response = await httpResponseMessage.DeserializeAsync<GetLoginStatusResponse>();

                LogInformation(
                    nameof(GetCustomerLoginStatusAsync),
                    $"Successfully processed {nameof(GetCustomerLoginStatusAsync)} {logSuffix} with response: {response.ToJsonString()}");
                return response;
            }
            
            catch (Exception ex)
            {
                var errorMessage = $"Failed to process {nameof(GetCustomerLoginStatusAsync)} {logSuffix} with message: {ex.Message}";
                LogError(ex, nameof(GetCustomerLoginStatusAsync), errorMessage);
                throw new CustomerCoreApiException(errorMessage, ex);
            }
        }

        public async Task<UpdateLoginStatusResponse> DisableCustomerLoginAsync(
            UpdateLoginStatusRequest request,
            CancellationToken cancellationToken)
        {
            var logSuffix = ($"for request {request.ToJsonString()}");
            try
            {
                LogInformation(
                    nameof(DisableCustomerLoginAsync),
                    $"Start processing {nameof(DisableCustomerLoginAsync)} {logSuffix}");
                var httpResponseMessage = await _customerCoreServiceProxy.PostDisableLoginAsync(request, cancellationToken);
                
                httpResponseMessage.EnsureSuccess();
                var response = await httpResponseMessage.DeserializeAsync<UpdateLoginStatusResponse>();

                LogInformation(
                    nameof(DisableCustomerLoginAsync),
                    $"Successfully processed {nameof(DisableCustomerLoginAsync)} {logSuffix} with response: {response.ToJsonString()}");

                return response;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to process {nameof(DisableCustomerLoginAsync)} {logSuffix} with message: {ex.Message}";
                LogError(ex, nameof(DisableCustomerLoginAsync), errorMessage);
                throw new CustomerCoreApiException(errorMessage, ex);
            }
        }

        public async Task<UpdateLoginStatusResponse> EnableCustomerLoginAsync(UpdateLoginStatusRequest request, CancellationToken cancellationToken)
        {
            var logSuffix = $"for request {request.ToJsonString()}";
            try
            {
                LogInformation(
                    nameof(EnableCustomerLoginAsync),
                    $"Start processing {nameof(EnableCustomerLoginAsync)} {logSuffix}");
                var httpResponseMessage = await _customerCoreServiceProxy.PostEnableLoginAsync(request, cancellationToken);

                httpResponseMessage.EnsureSuccess();
                var response = await httpResponseMessage.DeserializeAsync<UpdateLoginStatusResponse>();

                LogInformation(
                    nameof(DisableCustomerLoginAsync),
                    $"Successfully processing {nameof(EnableCustomerLoginAsync)} {logSuffix} with response: {response.ToJsonString()}");
                return response;
            }
            catch(Exception ex)
            {
                var errorMessage = $"Failed to process {nameof(EnableCustomerLoginAsync)} {logSuffix} with message: {ex.Message}";
                LogError(ex, nameof(EnableCustomerLoginAsync), errorMessage);
                throw new CustomerCoreApiException(errorMessage, ex);
            }
        }
    }
}