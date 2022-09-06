using Zip.Api.CustomerSummary.Domain.Entities.Payments;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule.Models
{
    public class GetRepaymentScheduleQueryResult
    {
        public RepaymentSchedule Schedule { get; set; }

        public NextPayment NextRepayment { get; set; }
    }
}
