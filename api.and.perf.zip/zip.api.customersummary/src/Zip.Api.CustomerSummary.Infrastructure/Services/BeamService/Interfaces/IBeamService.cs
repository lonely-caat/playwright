using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.BeamService.Interfaces
{
    public interface IBeamService
    {
        Task<CustomerDetails> GetCustomerDetails(Guid customerId, string region = Regions.Australia);

        Task<RewardActivityResponse> GetRewardActivityAsync(Guid customerId, long skip, long take, string region = Regions.Australia);

        Task<TransactionRewardDetailsResponse> GetTransactionRewardDetailsAsync(Guid customerId, long transactionId);

        Task<CreateReconciliationReportResponse> CreateReconciliationReportAsync(DateTime selectedDate, string requestedBy, string region = Regions.Australia);

        Task<PollReconciliationReportResponse> PollReconciliationReportAsync(Guid reportId, string requestedBy);
    }
}
