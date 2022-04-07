using Refit;
using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces
{
    public interface IBeamProxy
    {
        [Get("/system-api/v1/ping")]
        Task<string> Ping();
        
        [Get("/trusted-application-api/v1/secure/zip/customers/{customerId}?region={region}")]
        Task<CustomerDetails> GetCustomerDetailsAsync(Guid customerId, [Query] string region);

        [Get("/trusted-application-api/v1/secure/zip/customers/{customerId}/reward/activity?skip={skip}&take={take}&region={region}")]
        Task<RewardActivityResponse> GetRewardActivityAsync(Guid customerId, [Query] long skip, [Query] long take, [Query] string region);

        [Get("/trusted-application-api/v1/secure/zip/customers/{customerId}/transactions/{transactionId}/reward/activity")]
        Task<TransactionRewardDetailsResponse> GetTransactionRewardDetailsAsync([Query] Guid customerId, [Query] long transactionId);

        [Post("/trusted-application-api/v1/secure/zip/ledger-reconciliation-reports/export?year={year}&month={month}&region={region}")]
        Task<CreateReconciliationReportResponse> CreateReconciliationReportAsync([Query] long year, [Query] long month, [Header("From")] string requestedBy, [Query] string region);

        [Get("/trusted-application-api/v1/secure/zip/ledger-reconciliation-reports/{reportId}/poll")]
        Task<PollReconciliationReportResponse> PollReconciliationReportAsync([Query] Guid reportId, [Header("From")] string requestedBy);
    }
}
