using MediatR;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule.Models;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule
{
    public class GetRepaymentScheduleQuery : IRequest<GetRepaymentScheduleQueryResult>
    {
        public long AccountId { get; }
        public GetRepaymentScheduleQuery(long accountId)
        {
            AccountId = accountId;
        }

        public GetRepaymentScheduleQuery()
        {

        }
    }
}
