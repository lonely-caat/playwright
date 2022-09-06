using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Tango;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockTangoProxy : ITangoProxy
    {
        public Task<LoanMgtAccount> GetAccountAsync(long accountId, [Query(Format = "dd-MMM-yyyy")] DateTime asAtDate)
        {
            return Task.FromResult(new LoanMgtAccount());
        }

        public Task<IEnumerable<LoanMgtRepaymentScheduleVariation>> GetRepaymentScheduleByIdAsync(long directDebitId)
        {
            return Task.FromResult(new List<LoanMgtRepaymentScheduleVariation>() {
            new LoanMgtRepaymentScheduleVariation()
            {
                AccountHash= "1929828",
                DirectDebitId = 19281,
                RepaymentVariationStart = DateTime.Now.AddDays(12),
                VariationHasPrecedence = true,
                OverrideRepaymentAmount = 44,
            }
            }.AsEnumerable());
        }

        public Task<IEnumerable<LoanMgtRepaymentScheduleVariation>> ListAllRepaymentSchedulesAsync([Query] string accountHash)
        {
            return Task.FromResult(new List<LoanMgtRepaymentScheduleVariation>() {
            new LoanMgtRepaymentScheduleVariation()
            {
                AccountHash= "1929828",
                DirectDebitId = 19281,
                RepaymentVariationStart = DateTime.Now.AddDays(12),
                VariationHasPrecedence = true,
                OverrideRepaymentAmount = 44,
            }
            }.AsEnumerable());

        }

        public Task<HttpResponseMessage> UpdateRepaymentScheduleAsync(long directDebitId, [Body] LoanMgtRepaymentScheduleVariation variation)
        {
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
        }

        public Task<HttpResponseMessage> AddRepaymentScheduleAsync([Body] LoanMgtRepaymentScheduleVariation variation)
        {
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
        }
    }
}
