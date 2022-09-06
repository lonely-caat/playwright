using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Tango
{
    public interface ITangoProxy
    {
        [Get("/api/accounts/{accountId}")]
        Task<LoanMgtAccount> GetAccountAsync(long accountId, [Query(Format = "dd-MMM-yyyy")]DateTime asAtDate);

        [Get("/api/variations")]
        Task<IEnumerable<LoanMgtRepaymentScheduleVariation>> ListAllRepaymentSchedulesAsync([Query]string accountHash);

        [Post("/api/variations")]
        Task<HttpResponseMessage> AddRepaymentScheduleAsync([Body]LoanMgtRepaymentScheduleVariation variation);

        [Get("/api/variations/{directDebitId}")]
        Task<IEnumerable<LoanMgtRepaymentScheduleVariation>> GetRepaymentScheduleByIdAsync(long directDebitId);

        [Put("/api/variations/{directDebitId}")]
        Task<HttpResponseMessage> UpdateRepaymentScheduleAsync(long directDebitId, [Body]LoanMgtRepaymentScheduleVariation variation);
    }
}
