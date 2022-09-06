using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.BeamService
{
    public class BeamService : BaseService<BeamService>, IBeamService
    {
        private readonly IBeamProxy _beamProxy;

        public BeamService(
            IBeamProxy beamProxy,
            ILogger<BeamService> logger): base(logger)
        {
            _beamProxy = beamProxy;
        }

        public async Task<CustomerDetails> GetCustomerDetails(Guid customerId, string region = Regions.Australia)
        {
            LogInformation(
                nameof(GetCustomerDetails),
                $"Start {nameof(GetCustomerDetails)} with CustomerId: {customerId}");

            try
            {
                var response = await _beamProxy.GetCustomerDetailsAsync(customerId, region);

                LogInformation(
                    nameof(GetCustomerDetails),
                    $"Finished {nameof(GetCustomerDetails)} of CustomerId: {customerId} with response: {response.ToJsonString()}");

                return response;
            }
            catch (Refit.ApiException rex)
            {
                if (rex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    LogInformation(
                        nameof(GetCustomerDetails),
                        $"Finished {nameof(GetCustomerDetails)} of CustomerId: {customerId}. Customer details not found.");

                    return null;
                }

                LogError(rex,
                    nameof(GetCustomerDetails),
                    $"Failed to {nameof(GetCustomerDetails)} of CustomerId: {customerId}. " +
                    $"Refit API exception with message: {rex.Message}");

                throw new BeamApiException(rex.Message, rex);
            }
            catch (Exception ex)
            {
                LogError(ex,
                    nameof(GetCustomerDetails),
                    $"Failed to {nameof(GetCustomerDetails)} of CustomerId: {customerId} with message: {ex.Message}");

                throw;
            }
        }

        public async Task<TransactionRewardDetailsResponse> GetTransactionRewardDetailsAsync(Guid customerId, long transactionId)
        {
            var logSuffix = $"for CustomerId: {customerId}, TransactionId: {transactionId}";
            
            LogInformation(
                nameof(GetTransactionRewardDetailsAsync),
                $"Start {nameof(GetTransactionRewardDetailsAsync)} {logSuffix}");

            try
            {
                var response = await _beamProxy.GetTransactionRewardDetailsAsync(customerId, transactionId);

                LogInformation(
                    nameof(GetTransactionRewardDetailsAsync),
                    $"Finished {nameof(GetTransactionRewardDetailsAsync)} {logSuffix} with Response: {response.ToJsonString()}");

                return response;
            }
            catch (Refit.ApiException rex)
            {
                if (rex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    LogInformation(
                        nameof(GetTransactionRewardDetailsAsync),
                        $"Finished {nameof(GetTransactionRewardDetailsAsync)} for CustomerId: {customerId} and TransactionId: {transactionId}. Not Found.");

                    return null;
                }

                LogError(rex,
                         nameof(GetTransactionRewardDetailsAsync),
                         $"Failed to {nameof(GetTransactionRewardDetailsAsync)} {logSuffix} " +
                         $"Refit API exception with Message: {rex.Message}");

                throw new BeamApiException(rex.Message, rex);
            }
            catch (Exception ex)
            {
                LogError(ex,
                         nameof(GetTransactionRewardDetailsAsync),
                         $"Failed to {nameof(GetTransactionRewardDetailsAsync)} {logSuffix} with Message: {ex.Message}");

                throw;
            }
        }

        public async Task<CreateReconciliationReportResponse> CreateReconciliationReportAsync(DateTime selectedDate, string requestedBy, string region = Regions.Australia)
        {
            LogInformation(
                nameof(CreateReconciliationReportAsync),
                $"Start {nameof(CreateReconciliationReportAsync)} for selected date: {selectedDate} requested by: {requestedBy}");

            try
            {
                var response = await _beamProxy.CreateReconciliationReportAsync(selectedDate.Year, selectedDate.Month, requestedBy, region);

                LogInformation(
                    nameof(CreateReconciliationReportAsync),
                    $"Finished {nameof(CreateReconciliationReportAsync)} for selected date {selectedDate} " +
                    $"requested by: {requestedBy} with response: {response.ToJsonString()}");

                return response;
            }
            catch (Refit.ApiException rex)
            {
                LogError(rex,
                    nameof(CreateReconciliationReportAsync),
                    $"Failed to {nameof(CreateReconciliationReportAsync)} for selected date: {selectedDate} " +
                    $"requested by: {requestedBy}. Refit API exception with message: {rex.Message}");

                throw new BeamApiException(rex.Message, rex);
            }
            catch (Exception ex)
            {
                LogError(ex,
                    nameof(CreateReconciliationReportAsync),
                    $"Failed to {nameof(CreateReconciliationReportAsync)} for selected date {selectedDate} " +
                    $"requested by: {requestedBy} with message: {ex.Message}");

                throw;
            }
        }

        public async Task<RewardActivityResponse> GetRewardActivityAsync(Guid customerId, long skip, long take, string region = Regions.Australia)
        {
            LogInformation(
                nameof(GetRewardActivityAsync),
                $"Start {nameof(GetRewardActivityAsync)} for CustomerId: {customerId}");

            try
            {
                var response = await _beamProxy.GetRewardActivityAsync(customerId, skip, take, region);

                LogInformation(
                    nameof(GetRewardActivityAsync),
                    $"Finished {nameof(GetRewardActivityAsync)} for CustomerId: {customerId} with response: {response.ToJsonString()}");

                return response;
            }
            catch (Refit.ApiException rex)
            {
                if (rex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    LogInformation(
                        nameof(GetRewardActivityAsync),
                        $"Finished {nameof(GetRewardActivityAsync)} for CustomerId: {customerId}. Not found.");

                    return null;
                }

                LogError(rex,
                    nameof(GetRewardActivityAsync),
                    $"Failed to {nameof(GetRewardActivityAsync)} for CustomerId: {customerId}. " +
                    $"Refit API exception with message: {rex.Message}");

                throw new BeamApiException(rex.Message, rex);
            }
            catch (Exception ex)
            {
                LogError(ex,
                    nameof(GetRewardActivityAsync),
                    $"Failed to {nameof(GetRewardActivityAsync)} for CustomerId: {customerId} with message: {ex.Message}");

                throw;
            }
        }

        public async Task<PollReconciliationReportResponse> PollReconciliationReportAsync(Guid reportId, string requestedBy)
        {
            LogInformation(
                nameof(PollReconciliationReportAsync),
                $"Start {nameof(PollReconciliationReportAsync)} ReportId: {reportId} for {requestedBy}");

            try
            {
                var response = await _beamProxy.PollReconciliationReportAsync(reportId, requestedBy);

                LogInformation(
                    nameof(PollReconciliationReportAsync),
                    $"Finished {nameof(PollReconciliationReportAsync)} ReportId: {reportId} for {requestedBy}. " +
                    $"with response: {response.ToJsonString()}");

                return response;
            }
            catch (Refit.ApiException rex)
            {
                LogError(rex,
                    nameof(PollReconciliationReportAsync),
                    $"Failed to {nameof(PollReconciliationReportAsync)} ReportId: {reportId} for {requestedBy}. " +
                    $"Refit API exception with message: {rex.Message}");

                throw new BeamApiException(rex.Message, rex);
            }
            catch (Exception ex)
            {
                LogError(ex,
                    nameof(GetRewardActivityAsync),
                    $"Failed to {nameof(PollReconciliationReportAsync)} ReportId: {reportId} for {requestedBy} with message: {ex.Message}");

                throw;
            }
        }
    }
}
