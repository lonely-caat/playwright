using System;
using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Application.Payments.Query.GetPayments
{
    public class GetPaymentsQuery : IRequest<IEnumerable<PaymentDto>>
    {
        public long AccountId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? PaymentBatchId { get; set; }

        public GetPaymentsQuery(long accountId, DateTime? fromDate, DateTime? toDate, Guid? paymentBatchId)
        {
            AccountId = accountId;
            FromDate = fromDate;
            ToDate = toDate;
            PaymentBatchId = paymentBatchId;
        }

        public GetPaymentsQuery()
        {

        }
    }
}
